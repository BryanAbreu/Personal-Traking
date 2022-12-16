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
using DAL.DAO;
using DAL.DTO;

namespace PersonalTraking
{
    public partial class FrmEmployeeList : Form
    {
        public FrmEmployeeList()
        {
            InitializeComponent();
        }

        private void tbUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isnumber(e);
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmEmployee frm = new FrmEmployee();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
          

            FillAllData();
            ClearFilter();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.EmployeeID == 0)
                MessageBox.Show("Please select an employee on table");
            else 
            {
                FrmEmployee frm = new FrmEmployee();
                frm.detail = detail;
                frm.IsUpdate  = true;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                FillAllData();
                ClearFilter();

            }
          
        }
        EmployeeDTO dto = new EmployeeDTO();
        private bool comboFull = false;
        public EmployeeDetailDTO detail = new EmployeeDetailDTO();
        void FillAllData()
        {
            dto = EmployeeBLL.GetAll();
            dataGridView1.DataSource = dto.Employees;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "User";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].HeaderText = "Departmen";
            dataGridView1.Columns[5].HeaderText = "Position";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].HeaderText = "Salary";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;

            comboFull = false;
            cbDepartment.DataSource = dto.Departments;
            cbDepartment.DisplayMember = "DepartamentName";
            cbDepartment.ValueMember = "ID";
            cbDepartment.SelectedIndex = -1;

            cbPosition.DataSource = dto.Positions;
            cbPosition.DisplayMember = "PositionName";
            cbPosition.ValueMember = "ID";
            cbPosition.SelectedIndex = -1;
            comboFull = true;

        }


        private void FrmEmployeeList_Load(object sender, EventArgs e)
        {
            FillAllData();
        }

        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                int DepartmentID = Convert.ToInt32(cbDepartment.SelectedValue);
                cbPosition.DataSource = dto.Positions.Where(x => x.DepartamentID == DepartmentID).ToList();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<EmployeeDetailDTO> list = dto.Employees;
                if (tbUser.Text.Trim() != "")
                list = list.Where(x => x.User == Convert.ToInt32(tbUser.Text)).ToList();
            if (tbName.Text.Trim() != "")
                list = list.Where(x => x.Name.Contains(tbName.Text)).ToList();
            if (tbSurname.Text.Trim() != "")
                list = list.Where(x => x.Surname.Contains(tbSurname.Text)).ToList();
            if (cbDepartment.SelectedIndex != -1)
                list = list.Where(x => x.DepartmentID == Convert.ToInt32(cbDepartment.SelectedValue)).ToList();
            if (cbPosition.SelectedIndex != -1)
                list = list.Where(x => x.PositionID == Convert.ToInt32(cbPosition.SelectedValue)).ToList();

            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFilter();
        }

        private void ClearFilter()
        {
            tbUser.Clear();
            tbName.Clear();
            tbSurname.Clear();
            comboFull = false;
            cbDepartment.SelectedIndex = -1;
            cbPosition.DataSource = dto.Positions;
            cbPosition.SelectedIndex = -1;
            comboFull = true;
            dataGridView1.DataSource = dto.Employees;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.User = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detail.Name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            detail.Surname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            detail.DepartmentID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
            detail.PositionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
            detail.Password = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            detail.Adress = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
            detail.BhirtDay = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[13].Value);
            detail.Salary = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
            detail.isAdmin = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detail.ImagePath = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();




        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete this Employee","Warnin!!",MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                EmployeeBLL.DeleteEmployee(detail.EmployeeID);
                MessageBox.Show("Employee was deleted");
                FillAllData();
                ClearFilter();
            
            }

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExcelExport(dataGridView1);
        }
    }
}
