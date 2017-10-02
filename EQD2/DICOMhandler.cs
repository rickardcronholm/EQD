using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using EvilDICOM.Network;
using EvilDICOM.Network.Helpers;
using EvilDICOM.Network.Querying;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Logging;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Selection;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Core.Modules;
using EvilDICOM.RT;

namespace EQD2
{
    class DICOMhandler
    {
        private string doseUID;
        private string id;
        private string planUID;
        private string tempPath;
        private Entity daemon;
        private DICOMSCU puller;
        private FileWriterSCP catcher;
        private QueryBuilder qb;
        private DICOMObject plan, dose;
        private double alphaBeta, dosePerFraction;
        private int nFractions;

        public DICOMhandler(string id, string planUID, string doseUID, string tempPath, Entity daemon, DICOMSCU puller, FileWriterSCP catcher, double alphaBeta, double dosePerFraction, int nFractions)
        {
            this.id = id;
            this.planUID = planUID;
            this.doseUID = doseUID;
            this.alphaBeta = alphaBeta;
            this.dosePerFraction = dosePerFraction;
            this.tempPath = tempPath;
            this.nFractions = nFractions;

            this.daemon = daemon;
            this.puller = puller;
            this.catcher = catcher;
            

            this.qb = new QueryBuilder(puller, daemon);

        }

        public void ReadFiles(string path)
        {
            plan = ReadPlan(path);
            dose = ReadDose(path);
        }

        public void DeleteFiles(string path)
        {
            File.Delete(path + this.planUID + ".dcm"); // plan
            File.Delete(path + this.doseUID + ".dcm"); // dose
        }

        private DICOMObject AddMetaData(DICOMObject dcm)
        {
            var metadata = new FileMetadata();
            byte[] b = new byte[2] { 0, 1 };
            metadata.GroupLength = 166;
            metadata.InfoVersion = b;
            metadata.ImplementationClassUID = "1.2.246.352.70.2.1.7";
            metadata.MediaStorageSOPClassUID = dcm.FindFirst(TagHelper.SOPCLASS_UID).DData.ToString();
            metadata.MediaStorageSOPInstanceUID = dcm.FindFirst(TagHelper.SOPINSTANCE_UID).DData.ToString();
            foreach (IDICOMElement el in metadata.Elements)
                if (el.DData_.Count > 0)
                    dcm.Add(el);
            return dcm;
        }

        public string WriteFiles(string path)
        {
         
            var settings = new DICOMWriteSettings();
            var meta = settings.GetFileMetaSettings();
            meta.TransferSyntax = EvilDICOM.Core.Enums.TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN;
            DICOMFileWriter.Write(path + "RP." + this.planUID + ".dcm", meta, plan);
            DICOMFileWriter.Write(path + "RD." + this.doseUID + ".dcm", meta, dose);
            //DICOMFileWriter.WriteLittleEndian(path + "RP." + this.planUID + ".dcm", plan);
            //DICOMFileWriter.WriteLittleEndian(path + this.doseUID + ".dcm", dose);
            return "Plan: RP." + this.planUID + ".dcm\r\nDose: RD." + this.doseUID + ".dcm\r\nWritten to " + path;
        }

        private DICOMObject ReadPlan(string path)
        {
            return ReadFile(path + this.planUID + ".dcm");
        }

        private DICOMObject ReadDose(string path)
        {
            return ReadFile(path + this.doseUID + ".dcm");
        }


        public string PlanUID
        {
            get { return this.planUID; }
        }

        public string DoseUID
        {
            get { return this.doseUID; }
        }

        public void PullFile(string SOPuid, string AEtitle)
        {
            ushort msgId = 1;
            qb.SendImage(new CFindImageIOD() { PatientId = this.id, SOPInstanceUID = SOPuid }, AEtitle, ref msgId);
        }

        private DICOMObject ReadFile(string fileName)
        {
            DICOMObject dcm = DICOMObject.Read(fileName);
            return dcm;
        }

        public void UpdatePlan()
        {
            string description = "EGD2 generated from " + this.planUID + "\r\nα/β = " + this.alphaBeta.ToString();
            var sel = new DICOMSelector(plan);
            if (sel.RTPlanDescription == null)
                sel.RTPlanDescription = new ShortText(TagHelper.RTPLAN_DESCRIPTION, description);
            else
                sel.RTPlanDescription.Data += description;
            sel.SOPInstanceUID.Data = UIDHelper.GenerateUID("1.2.246.352.71.5");
            sel.SeriesInstanceUID.Data = UIDHelper.GenerateUID("1.2.246.352.71.2");
            /*
            sel.MediaStorageSOPClassUID = new UniqueIdentifier(TagHelper.MEDIA_STORAGE_SOPCLASS_UID, sel.SOPClassUID.Data);
            sel.MediaStorageSOPInstanceUID = new UniqueIdentifier(TagHelper.MEDIA_STORAGE_SOPINSTANCE_UID, sel.SOPInstanceUID.Data);
            sel.ImplementationClassUID = new UniqueIdentifier(TagHelper.IMPLEMENTATION_CLASS_UID, UIDHelper.GenerateUID());
            */
            sel.ApprovalStatus.Data = "REJECTED";
            sel.RTPlanLabel.Data = "EQD2_" + sel.RTPlanLabel.Data.Trim();
            if (sel.RTPlanLabel.Data.Length > 13)
                sel.RTPlanLabel.Data = sel.RTPlanLabel.Data.Substring(0,13).Trim();
            plan = sel.ToDICOMObject();
            
            /*
            plan.FindFirst(TagHelper.SOPINSTANCE_UID).DData = UIDHelper.GenerateUID();
            plan.FindFirst(TagHelper.SERIES_INSTANCE_UID).DData = UIDHelper.GenerateUID();
            plan.FindFirst(TagHelper.RTPLAN_LABEL).DData = "EQD2_" + plan.FindFirst(TagHelper.RTPLAN_LABEL).DData;
            plan.FindFirst(TagHelper.APPROVAL_STATUS).DData = "REJECTED";
            this.planUID = plan.FindFirst(TagHelper.SOPINSTANCE_UID).DData.ToString();
            return plan.FindFirst(TagHelper.SOPINSTANCE_UID).DData.ToString();
            */
            this.planUID = sel.SOPInstanceUID.Data;

            // add metadata
            plan = AddMetaData(plan);
            //return this.planUID;
        }

        public void UpdateDose(string path)
        {
            // Compute new dose matrix
            string fileName = path + this.DoseUID + ".dcm";
            var DoseValues = computeEQD2(fileName, this.nFractions, this.alphaBeta);
            
            var sel = new DICOMSelector(dose);
            
            sel.SOPInstanceUID.Data = UIDHelper.GenerateUID("1.2.246.352.71.7");
            sel.SeriesInstanceUID.Data = UIDHelper.GenerateUID("1.2.246.352.71.2");
            /*
            dose.FindFirst(TagHelper.SOPINSTANCE_UID).DData = UIDHelper.GenerateUID();
            dose.FindFirst(TagHelper.SERIES_INSTANCE_UID).DData = UIDHelper.GenerateUID();
            this.doseUID = dose.FindFirst(TagHelper.SOPINSTANCE_UID).DData.ToString();
            return dose.FindFirst(TagHelper.SOPINSTANCE_UID).DData.ToString();
            */
            sel.ReferencedRTPlanSequence_[0].ReferencedSOPInstanceUID.Data = this.planUID;

            // overwrite dose matrix
            var _16b = 1 / Math.Pow(2, 16);
            sel.DoseGridScaling.Data = _16b;
            using (var stream = new MemoryStream())
            {
                var binWriter = new BinaryWriter(stream);
                foreach (var d in DoseValues)
                {
                    int integ = (int)(d / _16b);
                    var bytes = BitConverter.GetBytes(integ);
                    binWriter.Write(integ);
                }
                var ows = new OtherWordString(TagHelper.PIXEL_DATA, stream.ToArray());
                sel.ToDICOMObject().Replace(ows);
                
            }


            //dose = sel.ToDICOMObject();
            this.doseUID = sel.SOPInstanceUID.Data;

            // add metadata
            dose = AddMetaData(dose);

        }

        public void AnonUID()
        {
            var uidAnon = new EvilDICOM.Anonymization.Anonymizers.UIDAnonymizer();
            uidAnon.AddDICOMObject(this.plan);
            uidAnon.AddDICOMObject(this.dose);
            uidAnon.Anonymize(plan);
            uidAnon.Anonymize(dose);
        }


        private double computeEQD2(double doseGridScaling)
        {
            // EQD2 = D * (d + α/β) / (2 + α/β)
            return doseGridScaling * (this.dosePerFraction + this.alphaBeta) / (2.0 + this.alphaBeta);
        }

        private List<double> computeEQD2(string fileName, int nFractions, double alphaBeta)
        {
            var doseData = EvilDICOM.RT.DoseMatrix.Load(fileName);
            
            for (int i = 0; i < doseData.DoseValues.Count; i++)
            {
                doseData.DoseValues[i] = doseData.DoseValues[i] * (doseData.DoseValues[i] / nFractions + alphaBeta) / (2.0 + alphaBeta);
            }

            return doseData.DoseValues;
            
            
        }
    }
}
