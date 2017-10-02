using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Logging;
using EvilDICOM.Network;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Helpers;
using EvilDICOM.Network.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vapi = VMS.TPS.Common.Model.API;
using System.Diagnostics;
using System.IO;

namespace EQD2
{
    class Program
    {
        static Entity daemon = new Entity("ARIADB", "10.0.129.139", 57347);

        [STAThread]
        static void Main(string[] args)
        {
            var process = Process.GetCurrentProcess(); 
            string fullPath = Path.GetDirectoryName(process.MainModule.FileName);
            var me = Entity.CreateLocal("DCMGRBC", 51167);
            var scu = new DICOMSCU(me);
            var path = fullPath + @"\temp\";
            var scpEntity = Entity.CreateLocal("DCMGRB2", 50401);
            var scp = new FileWriterSCP(scpEntity, path);
            scp.SupportedAbstractSyntaxes = AbstractSyntax.ALL_RADIOTHERAPY_STORAGE;
            scp.ListenForIncomingAssociations(true);
            var logger = new ConsoleLogger(scp.Logger, ConsoleColor.White);
            try
            {
                using (var app = vapi.Application.CreateApplication(null, null))
                {
                    Execute(app, daemon, scu, scp, path);
                }

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString(), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        static void Execute(vapi.Application app, Entity daemon, DICOMSCU scu, FileWriterSCP scp, string path)
        {
            SelectPlanUI selectPlanUI = new SelectPlanUI(app, daemon, scu, scp, path);
            selectPlanUI.ShowDialog();
        }
    }
}
