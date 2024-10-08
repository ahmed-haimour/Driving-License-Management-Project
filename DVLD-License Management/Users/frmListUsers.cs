using DVLD_Buisness;
using DVLD_License_Management.Global_Classes;
using DVLD_License_Management.Login;
using DVLD_License_Management.People;
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
    public partial class frmListUsers : Form
    {
        private static DataTable _dtAllUsers = clsUser.GetAllUsers();

        private DataTable _dtUsers = _dtAllUsers.DefaultView.ToTable(false, "UserID","PersonID", "FullName",
                                                         "UserName", "IsActive");

        public frmListUsers()
        {
            InitializeComponent();
        }

        private void _RefreshPeopleListByGendor(bool isActive)
        {
            dgvUsers.DataSource = clsUser.GetAllUsersByActive(isActive);
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            cbIsActive.Visible = false;
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();

            if (dgvUsers.Rows.Count > 0)
            {

                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 110;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 120;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 350;

                dgvUsers.Columns[3].HeaderText = "UserName";
                dgvUsers.Columns[3].Width = 120;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 120;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;

                case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "UserID" || FilterColumn == "PersonID")
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());


            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvUsers.DataSource = _dtUsers;
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();

            if (cbFilterBy.SelectedIndex == 0)
                cbFilterBy.Focus();
            else
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            if (cbFilterBy.SelectedIndex == 5)
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.SelectedIndex = 0;
                cbIsActive.Focus();
            }
            else
            {
                cbIsActive.Visible = false;
                txtFilterValue.Visible = true;
                //txtFilterValue.Focus();
            }
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateNewUser();
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 5;
            cbFilterBy_SelectedIndexChanged(null, null);
            cbIsActive.SelectedIndex = 1;
        }

        private void btnInActive_Click(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 5;
            cbFilterBy_SelectedIndexChanged(null, null);
            cbIsActive.SelectedIndex = 2;
        }

        private void cbIsActive_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbIsActive.Text)
            {
                case "All":
                    FilterColumn = "All";
                    break;

                case "Yes":
                    FilterColumn = "Yes";
                    break;

                case "No":
                    FilterColumn = "No";
                    break;

                default:
                    FilterColumn = "All";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (FilterColumn == "All")
            {
                dgvUsers.DataSource = _dtUsers;
                _dtAllUsers = clsUser.GetAllUsers();
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "Yes")
             _RefreshPeopleListByGendor(true);
            else
                _RefreshPeopleListByGendor(false);

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void cmEdit_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateNewUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void cmAddNewUser_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateNewUser();
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void cmDelete_Click_1(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            if (clsGlobal.CurrentUser.UserID == UserID)
            {
                MessageBox.Show("You cannot delete this account when you are logged in to it", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsUser.DeleteUser(UserID))
            {
                MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListUsers_Load(null, null);
            }
            else
                MessageBox.Show("User is not deleted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cmChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void cmShowDetails_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void cmSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void cmPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
