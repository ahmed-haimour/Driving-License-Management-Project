using DVLD_Buisness;
using DVLD_License_Management.Applications.Application_Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_License_Management.Tests.Test_Types
{
    public partial class frmListTestTypes : Form
    {
        private static DataTable _dtAllTestTypes = clsTestType.GetAllTestTypes();
        //only select the columns that you want to show in the grid
        private DataTable _dtTestTypes = _dtAllTestTypes.DefaultView.ToTable(false, "TestTypeID", "TestTypeTitle",
                                                         "TestTypeDescription", "TestTypeFees");

        private void _RefreshTistType()
        {
            _dtAllTestTypes = clsTestType.GetAllTestTypes();
            _dtTestTypes = _dtAllTestTypes.DefaultView.ToTable(false, "TestTypeID", "TestTypeTitle",
                                                         "TestTypeDescription", "TestTypeFees");
            dgvTestTypes.DataSource = _dtTestTypes;
            lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();
        }

        public frmListTestTypes()
        {
            InitializeComponent();
        }


        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            dgvTestTypes.DataSource = _dtTestTypes;
            lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();

            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 80;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 160;

                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 370;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 140;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestTypes frm = new frmEditTestTypes((int)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshTistType();
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
