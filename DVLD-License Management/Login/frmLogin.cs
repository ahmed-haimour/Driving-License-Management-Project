using DVLD_Buisness;
using DVLD_License_Management.Global_Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DVLD_License_Management.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private int _FieldLoginTrials = 0;
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            
            if (User != null)
            {
                if (cbRememberMe.Checked)
                {
                    //store username and password
                    clsGlobal.RememberUsernameAndPasswordUsingRegistry(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    //store empty username and password
                    clsGlobal.RememberUsernameAndPasswordUsingRegistry("", "");
                }

                //incase the user is not active
                if (!User.IsActive)
                {

                    txtUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _FieldLoginTrials = 0;
                clsGlobal.CurrentUser = User;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();

            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _FieldLoginTrials++;

                if (_FieldLoginTrials >= 3)
                {
                    clsGlobal.SaveToEventLog($"{_FieldLoginTrials} Field Login Trials!", EventLogEntryType.Warning);
                }
            }
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(230, 8, 9, 49);
            
            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredentialUsingRegistry(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                cbRememberMe.Checked = true;
            }
            else
                cbRememberMe.Checked = false;

        }

        private void miniMaize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
