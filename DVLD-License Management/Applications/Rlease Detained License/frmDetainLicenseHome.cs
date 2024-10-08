using DVLD_License_Management.Applications.Local_Driving_License;
using DVLD_License_Management.Licenses.Detain_License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_License_Management.Applications.Rlease_Detained_License
{
    public partial class frmDetainLicenseHome : Form
    {
        frmApplications frmApplications;
        public frmDetainLicenseHome(frmApplications frmApplications)
        {
            InitializeComponent();
            this.frmApplications = frmApplications;
        }

        private void pbBackApplications_Click(object sender, EventArgs e)
        {
            this.Close();
            Panel panel = frmApplications.GetPanel();
            panel.Hide();
        }

        public Panel GetPanel()
        {
            return panelDetainLic;
        }


        public void LoadForm(object Form)
        {

            if (this.panelDetainLic.Controls.Count > 0)
                this.panelDetainLic.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelDetainLic.Controls.Add(f);
            this.panelDetainLic.Tag = f;
            f.Show();
        }


        private void btnManageDetainedLicenses_Click(object sender, EventArgs e)
        {
            panelDetainLic.Visible = true;
            panelDetainLic.BringToFront();
            LoadForm(new frmListDetainedLicenses(this));
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void btnReleaseDetainLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }
    }
}
