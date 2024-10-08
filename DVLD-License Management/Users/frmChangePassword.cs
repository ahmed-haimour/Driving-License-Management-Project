using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_License_Management.Users
{
    public partial class frmChangePassword : Form
    {
        private clsUser _User;
        private int _UserID = -1;

        public int UserID
        {
            get { return _UserID; }
        }

        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void _ResetDefulteValue()
        {
            txtCurrentPassword.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefulteValue();

            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Could not Find User with id = " + _UserID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;

            }

            ctrlUserCard1.LoadUserInfo(_UserID);

        }
            
        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            };

            if (_User.Password != txtCurrentPassword.Text.Trim())
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current password is wrong!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            };
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim())){
                //e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password cannot be blank");
            }
            else if (txtPassword.Text.Trim().Length < 4){
                //e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Your password is weak, must be more than 3 characters!");
            }
            else
                errorProvider1.SetError(txtPassword, null);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim())) {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password cannot be blank");
            }
            else if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim()) {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
                errorProvider1.SetError(txtConfirmPassword, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.Password = txtPassword.Text;

            if (_User.SaveChangePsasword(txtPassword.Text))
            {
                
                MessageBox.Show("Password Changed Succefully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefulteValue();
            }
            else
                MessageBox.Show("An Error Ocured, Password did not change", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
                this.Close();
        }
    }
}
