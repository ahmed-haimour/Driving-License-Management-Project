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

namespace DVLD_License_Management.Applications.Application_Types
{
    public partial class frmListApplicationTypes : Form
    {
        private static DataTable _dtAllApplicationType = clsApplicationType.GetAllApplicationType();

        //only select the columns that you want to show in the grid
        private DataTable _dtApplicationType = _dtAllApplicationType.DefaultView.ToTable(false, "ApplicationTypeID", "ApplicationTypeTitle",
                                                         "ApplicationFees");

        private void _RefreshApplicationType()
        {
            _dtAllApplicationType = clsApplicationType.GetAllApplicationType();
            _dtApplicationType = _dtAllApplicationType.DefaultView.ToTable(false, "ApplicationTypeID", "ApplicationTypeTitle",
                                                         "ApplicationFees");
            dgvApplicationTypes.DataSource = _dtApplicationType;
            lblRecordsCount.Text = dgvApplicationTypes.Rows.Count.ToString();
        }

        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            dgvApplicationTypes.DataSource = _dtApplicationType;
            lblRecordsCount.Text = dgvApplicationTypes.Rows.Count.ToString();

            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 100;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 210;


                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 140;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshApplicationType();
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
