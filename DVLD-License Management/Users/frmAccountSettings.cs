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
    public partial class frmAccountSettings : Form
    {
        clsUser _User;
        private int _UserID = -1;

        public frmAccountSettings(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void frmAccountSettings_Load(object sender, EventArgs e)
        {
            ctrlUserCard1.LoadUserInfo(_UserID);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(_UserID);
            frm.ShowDialog();
        }
    }
}
