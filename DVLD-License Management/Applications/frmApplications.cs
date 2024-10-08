using DVLD_License_Management.Applications.Application_Types;
using DVLD_License_Management.Applications.International_License;
using DVLD_License_Management.Applications.Local_Driving_License;
using DVLD_License_Management.Applications.Renew_Local_License;
using DVLD_License_Management.Applications.ReplaceLostOrDamagedLicense;
using DVLD_License_Management.Applications.Rlease_Detained_License;
using DVLD_License_Management.Tests.Test_Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_License_Management.Applications
{
    public partial class frmApplications : Form
    {
        public frmApplications()
        {
            InitializeComponent();
        }

        private void btnManageApplicationsTypes_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void btnManageTestTypes_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void btnDrivingLicenseServices_Click(object sender, EventArgs e)
        {
            panelServices.Visible = true;
            panelServices.BringToFront();
        }

        private void pbBackServices_Click(object sender, EventArgs e)
        {
            panelServices.Visible = false;
        }

        private void btnNewDrivingLicense_Click(object sender, EventArgs e)
        {
            panelTypeLicense.Visible = true;
            panelTypeLicense.BringToFront();
        }

        private void pbBackTypeLicense_Click(object sender, EventArgs e)
        {
            panelTypeLicense.Visible = false;
        }

        private void btnLocalLicense_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
        }

        private void btnManageApplications_Click(object sender, EventArgs e)
        {
            panelApplications.Visible = true;
            panelApplications.BringToFront();
        }

        private void pbBackApplications_Click(object sender, EventArgs e)
        {
            panelApplications.Visible = false;
        }

        public void LoadForm(object Form)
        {

            if (this.pListLocalDrivingLicesnseApplications.Controls.Count > 0)
                this.pListLocalDrivingLicesnseApplications.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.pListLocalDrivingLicesnseApplications.Controls.Add(f);
            this.pListLocalDrivingLicesnseApplications.Tag = f;
            f.Show();
        }

        public Panel GetPanel()
        {
            return pListLocalDrivingLicesnseApplications;
        }

        private void btnLocalDrivingLicenseApplications_Click(object sender, EventArgs e)
        {
            pListLocalDrivingLicesnseApplications.Visible = true;
            pListLocalDrivingLicesnseApplications.BringToFront();
            LoadForm(new frmListLocalDrivingLicesnseApplications(this));
        }

        private void btnInternationalLicense_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void btnInternationalDrivingLicenseApplications_Click(object sender, EventArgs e)
        {
            pListLocalDrivingLicesnseApplications.Visible = true;
            pListLocalDrivingLicesnseApplications.BringToFront();
            LoadForm(new frmListInternationalLicesnseApplications(this));
        }

        private void btnRnewDrivingLicense_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void btnReplacementforLostorDamagedLicense_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }

        private void btnDetainLicences_Click(object sender, EventArgs e)
        {
            pListLocalDrivingLicesnseApplications.Visible = true;
            pListLocalDrivingLicesnseApplications.BringToFront();
            LoadForm(new frmDetainLicenseHome(this));
        }

    }
}
