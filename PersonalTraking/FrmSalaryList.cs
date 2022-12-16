using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.DTO;
using BLL;
using DAL;

namespace PersonalTraking
{
    public partial class FrmSalaryList : Form
    {
        public FrmSalaryList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmSalary frm  = new FrmSalary();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            FillAllData();
            cleanFilter();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.SalaryID == 0)
                MessageBox.Show("Please select a salary from table");
            else
            {

                FrmSalary frm = new FrmSalary();
                frm.IsUpdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                FillAllData();
                cleanFilter();
            }
            
        }

        SalaryDTO dto = new SalaryDTO();
        bool comboFull = false;

        void FillAllData()
        {
            dto = SalaryBLL.GetAll();
            if (!UserStatic.IsAdmin)
                dto.Salaries = dto.Salaries.Where(x => x.EmployeeID == UserStatic.EmployeeID).ToList();
            dataGridView1.DataSource = dto.Salaries;
        
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

            if (dto.Departments.Count > 0)
                comboFull = true;
            cbMonth.DataSource = dto.Months;
            cbMonth.DisplayMember = "MonthName";
            cbMonth.ValueMember = "ID";
            cbMonth.SelectedIndex = -1;
        }
        SalaryDetailDTO detail = new SalaryDetailDTO();
        private void FrmSalaryList_Load(object sender, EventArgs e)
        {
            FillAllData();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "User";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].HeaderText = "Month";
            dataGridView1.Columns[9].HeaderText = "Year";
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].HeaderText = "Salary";
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;

            if (!UserStatic.IsAdmin)
            {
                btnDelete.Hide();
                btnUpdate.Hide();
                pnlForAdmin.Hide();

            }

           
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
            List<SalaryDetailDTO> list = dto.Salaries;
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
            if (tbYear.Text.Trim() != "")
                list = list.Where(x => x.SalaryYear == Convert.ToInt32(tbYear.Text)).ToList();
            if (cbMonth.SelectedIndex != -1)
                list = list.Where(X => X.MonthID == Convert.ToInt32(cbMonth.SelectedValue)).ToList();
            if (tbSalary.Text.Trim() != "")
            {
                if (rbMore.Checked)
                    list = list.Where(x => x.SalaryAmount > Convert.ToInt32(tbSalary.Text)).ToList();
                else if (rbLess.Checked)
                    list = list.Where(x => x.SalaryAmount < Convert.ToInt32(tbSalary.Text)).ToList();
                else
                    list = list.Where(x => x.SalaryAmount == Convert.ToInt32(tbSalary.Text)).ToList();

            }

            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cleanFilter();
        }

        private void cleanFilter()
        {
            tbUser.Clear();
            tbName.Clear();
            tbSurname.Clear();
            comboFull = false;
            cbDepartment.SelectedIndex = -1;
            cbPosition.DataSource = dto.Positions;
            cbPosition.SelectedIndex = -1;
            comboFull = true;
            cbMonth.SelectedIndex = -1;
            rbMore.Checked = false;
            rbLess.Checked = false;
            rbEqual.Checked = false;
            tbYear.Clear();
            tbSalary.Clear();
            dataGridView1.DataSource = dto.Salaries;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.Name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            detail.Surname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            detail.User = Convert.ToInt32( dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detail.SalaryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
            detail.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.SalaryYear = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detail.MonthID= Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            detail.SalaryAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
            detail.OldSalary = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[11].Value);


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete this salary", "Warning", MessageBoxButtons.YesNo);
            if (DialogResult.Yes == result)
            {
                SalaryBLL.DeleteSalary(detail.SalaryID);
                MessageBox.Show("Salary was deleted");
                FillAllData();
                cleanFilter();
              ;

            }
        }
    }
}
