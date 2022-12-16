using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL.DTO;

namespace PersonalTraking
{
    public partial class FrmPositionList : Form
    {
        public FrmPositionList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmPosition frm = new FrmPosition();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            FillGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.ID == 0)
                MessageBox.Show("Please select a position from table");
            else
            {
                FrmPosition frm = new FrmPosition();
                frm.IsUpdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                FillGrid();
                
            }
            
        }
        List<PositionDTO> positionList = new List<PositionDTO>();

        void FillGrid()
        {
            positionList = PositionBLL.GetPositions();
            dataGridView1.DataSource = positionList;

        }
        PositionDTO detail = new PositionDTO();
        private void FrmPositionList_Load(object sender, EventArgs e)
        {
            FillGrid();
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[0].HeaderText = "Department Name";
            dataGridView1.Columns[3].HeaderText = "Position Name";
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.PositionName = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            detail.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            detail.DepartamentID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            detail.OldDepartementID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete this position", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                PositionBLL.DeletePosition(detail.ID);
                MessageBox.Show("Position was deleted");
                FillGrid();
                
            
            }
        }
    }
}
