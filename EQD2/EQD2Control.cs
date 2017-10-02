using EvilDICOM.Core;
using EvilDICOM.Network;
using EvilDICOM.Network.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VMS.TPS.Common.Model.API;

namespace EQD2
{
    public partial class EQD2Control : Form
    {
        private User currentUser;
        private Patient patient;
        private PlanSetup plan;
        private double totalDose, dosePerFraction, alphaBeta = 3;
        private int nFractions;
        private string planUID, doseUID;
        //private string tempPath = @"C: \Users\150801\Desktop\DICOMdump\";
        private string tempPath;
        private string writePath = @"\\mtdb001\VA_TRANSFER\Aria export_import\EQD\";
        private Entity daemon;
        private DICOMSCU scu;
        private FileWriterSCP scp;

        private void buttonGenEQD2_Click(object sender, EventArgs e)
        {
            textBoxOut.Text = String.Empty;
            DICOMhandler dcmhndlr = new DICOMhandler(patient.Id, planUID, doseUID, tempPath, daemon, scu, scp, alphaBeta, dosePerFraction, nFractions);
            // pull files
            dcmhndlr.PullFile(dcmhndlr.PlanUID, scp.ApplicationEntity.AeTitle);
            dcmhndlr.PullFile(dcmhndlr.DoseUID, scp.ApplicationEntity.AeTitle);
            // read files
            dcmhndlr.ReadFiles(tempPath);
            // update UIDs and compute EQD2
            dcmhndlr.UpdatePlan();
            dcmhndlr.UpdateDose(tempPath);
            // delete temp files
            dcmhndlr.DeleteFiles(tempPath);
            // write files
            try
            {
                textBoxOut.Text = dcmhndlr.WriteFiles(writePath);
                //"Plan: RP." + this.planUID + ".dcm\r\nDose: RD." + this.doseUID + ".dcm\r\nWritten to " + writePath;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public EQD2Control(User currentUser, Patient pat, PlanSetup plan, Entity daemon, DICOMSCU scu, FileWriterSCP scp, string tempPath)
        {
            this.tempPath = tempPath;
            this.currentUser = currentUser;
            this.patient = pat;
            this.plan = plan;
            this.daemon = daemon;
            this.scu = scu;
            this.scp = scp;
            this.totalDose = Convert.ToDouble(plan.TotalPrescribedDose.ValueAsString);
            this.dosePerFraction = plan.UniqueFractionation.PrescribedDosePerFraction.Dose;
            this.nFractions = (int)plan.UniqueFractionation.NumberOfFractions;
            this.planUID = plan.UID;
            this.doseUID = plan.Dose.UID;

            InitializeComponent();

            // place info
            textBoxPat.Text = patient.ToString();
            textBoxPlan.Text = plan.ToString();
            textBoxFractionation.Text = plan.UniqueFractionation.PrescribedDosePerFraction.ToString() + " × " + this.nFractions + " = " + plan.TotalPrescribedDose.ToString();
            textBox1.Text = alphaBeta.ToString();
        }

        public double AlphaBeta
        {
            get { return this.alphaBeta; }
            set { this.alphaBeta = value; }
        }
            

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double value;
            try
            {
                Double.TryParse(textBox1.Text, out value);
                if (value <= 0 || value > 10)
                    throw new Exception("Value out of range.");
                this.AlphaBeta = value;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Text = this.alphaBeta.ToString();
            }
        }

    }
}
