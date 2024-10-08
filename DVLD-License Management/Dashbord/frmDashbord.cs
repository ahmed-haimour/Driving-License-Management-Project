using DVLD_Buisness;
using DVLD_License_Management.Global_Classes;
using DVLD_License_Management.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_License_Management.Dashbord
{
    public partial class frmDashbord : Form
    {
        private string text;
        private int len = 0;

        public frmDashbord()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(len < text.Length )
            {
                lblText.Text = lblText.Text + text.ElementAt(len);
                len++;
            }
            else
                timer1.Stop();
        }

        private void frmDashbord_Load(object sender, EventArgs e)
        {
            //clock
            timer2.Start();
            //---

            lblDay.Text = DateTime.Now.ToString("dddd, MMMM M, yyyy");

            text = lblText.Text;
            lblText.Text = "";
            timer1.Start();

            lblCountPeople.Text = clsPerson.CountPeople().ToString();
            lblCountDrivers.Text = clsDriver.CountDriver().ToString();
            lblCountUsers.Text = clsUser.CountUser().ToString();    
            lblCountLicences.Text = clsLicense.CountLicense().ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void lblDay_Click(object sender, EventArgs e)
        {

        }
    }
}
