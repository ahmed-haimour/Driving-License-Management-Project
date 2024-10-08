using DVLD_Buisness;
using DVLD_License_Management.Applications;
using DVLD_License_Management.Dashbord;
using DVLD_License_Management.Drivers;
using DVLD_License_Management.Global_Classes;
using DVLD_License_Management.Login;
using DVLD_License_Management.People;
using DVLD_License_Management.Properties;
using DVLD_License_Management.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_License_Management
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;

        public frmMain(frmLogin frm)
        {
            InitializeComponent();
            _frmLogin = frm;
        }
        
        public void LoadForm(object Form)
        {

            if (this.pMain.Controls.Count > 0)
                this.pMain.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.pMain.Controls.Add(f);
            this.pMain.Tag = f;
            f.Show();
        }

        private void btnPeople_Click(object sender, EventArgs e)
        {
            LoadForm(new frmListPeople());
        }

        private void btnDrivers_Click(object sender, EventArgs e)
        {
            LoadForm(new frmListDrivers());
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            LoadForm(new frmListUsers());
        }

        private void btnAccountSettings_Click(object sender, EventArgs e)
        {
            LoadForm(new frmAccountSettings(clsGlobal.CurrentUser.UserID));
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblNamePerson.Text = clsGlobal.CurrentUser.PersonInfo.FullName;
            pbImagePerson.ImageLocation = clsGlobal.CurrentUser.PersonInfo.ImagePath;

            if (clsGlobal.CurrentUser.PersonInfo.ImagePath == "")
                pbImagePerson.Image = Resources.user__13_;

            LoadForm(new frmDashbord());
        }

        private void btnApplications_Click(object sender, EventArgs e)
        {
            LoadForm(new frmApplications());
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            LoadForm(new frmDashbord());
        }

        private void pbClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void miniMaize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
