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
using DAL;

namespace PersonalTraking
{
    public partial class FrmDepartmentList : Form
    {
        public FrmDepartmentList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmDepartment frm = new FrmDepartment();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;

            list = DepartmentBLL.GetDepartments();
            dataGridView1.DataSource = list;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.ID == 0)
                MessageBox.Show("Please select a department from table");
            else
            {
                FrmDepartment frm = new FrmDepartment();
                frm.IsUpdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;

               
                list = DepartmentBLL.GetDepartments();
                dataGridView1.DataSource = list;

            }
           
        }
        List<DEPARTAMENT> list = new List<DEPARTAMENT>();
        public DEPARTAMENT detail = new DEPARTAMENT();
        private void FrmDepartmentList_Load(object sender, EventArgs e)
        {
            List<DEPARTAMENT> list = new List<DEPARTAMENT>();
            list = DepartmentBLL.GetDepartments();
            dataGridView1.DataSource= list;
            dataGridView1.Columns[0].Visible=false;
            dataGridView1.Columns[1].HeaderText = "Department Name";


        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.DepartamentName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete this Department","Warning",MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DepartmentBLL.DeleteDepartment(detail.ID);
                MessageBox.Show("Deparment was delete");

                list = DepartmentBLL.GetDepartments();
                dataGridView1.DataSource = list;
            }

        }
    }
}
