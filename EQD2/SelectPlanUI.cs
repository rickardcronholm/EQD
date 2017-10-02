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
    public partial class SelectPlanUI : Form
    {
        private VMS.TPS.Common.Model.API.Application app;
        private Entity daemon;
        private DICOMSCU scu;
        private FileWriterSCP scp;
        private string path;

        public SelectPlanUI(VMS.TPS.Common.Model.API.Application app, Entity daemon, DICOMSCU scu, FileWriterSCP scp, string path)
        {
            this.app = app;
            this.daemon = daemon;
            this.scu = scu;
            this.scp = scp;
            this.path = path;
            this.Location = new Point(0, 0);

            InitializeComponent();

            //textBoxPatId.Text = "QC_Checklista";
        }

        private void SelectPlanUI_Load(object sender, EventArgs e)
        {

        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            try
            {
                Patient pat = app.OpenPatientById(textBoxPatId.Text);
                listBoxCourse.Items.Clear();
                if (pat != null)
                {
                    textBoxPat.Tag = pat;
                    textBoxPat.Text = pat.ToString();
                    foreach (Course course in pat.Courses)
                        listBoxCourse.Items.Add(course);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                app.ClosePatient();
                textBoxPat.Text = string.Empty;
                listBoxCourse.Items.Clear();
                listBoxPlan.Items.Clear();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxPlan.Items.Clear();
            if (listBoxCourse.SelectedIndex != -1)
            {
                Course course = (Course)listBoxCourse.SelectedItem;
                foreach (PlanSetup plan in course.PlanSetups)
                    listBoxPlan.Items.Add(plan);
            }
        }

        private void listBoxPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPlan.SelectedIndex != -1)
            {
                Patient pat = (Patient)textBoxPat.Tag;
                PlanSetup plan = (PlanSetup)listBoxPlan.SelectedItem;
                EQD2Control eqd2c = new EQD2Control(app.CurrentUser, pat, plan, daemon, scu, scp, path);
                eqd2c.ShowDialog();
                //SelectPlanUI selectPlanUI = new SelectPlanUI(app);
                //selectPlanUI.ShowDialog();
            }
        }
    }
}
