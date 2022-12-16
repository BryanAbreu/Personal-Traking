using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using DAL.DTO;
using BLL;

namespace PersonalTraking
{
    public partial class FrmSalary : Form
    {
        public FrmSalary()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        SalaryDTO dto = new SalaryDTO();
        public bool comboFull = false;
        public SalaryDetailDTO detail = new  SalaryDetailDTO();
        public bool IsUpdate = false;

        private void FrmSalary_Load(object sender, EventArgs e)
        {
            dto = SalaryBLL.GetAll();
            if (!IsUpdate)
            {
                dataGridView1.DataSource = dto.Employees;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "User";
                dataGridView1.Columns[2].HeaderText = "Name";
                dataGridView1.Columns[3].HeaderText = "Surname";
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
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
                if (dto.Departments.Count > 0)
                    comboFull = true;
            }
            

           
            cbMonth.DataSource = dto.Months;
            cbMonth.DisplayMember = "MonthName";
            cbMonth.ValueMember = "ID";
            cbMonth.SelectedIndex = -1;

            if (IsUpdate)
            {
                panel1.Hide();
                tbUser.Text = detail.User.ToString();
                tbSurname.Text = detail.Surname.ToString();
                tbName.Text = detail.Name;
                tbSalary.Text = detail.SalaryAmount.ToString();
                tbYear.Text = detail.SalaryYear.ToString();
                cbMonth.SelectedValue = detail.MonthID;

            }

        }
        SALARY salary = new SALARY();
        int oldsalary = 0;
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            tbUser.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            tbName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            tbSurname.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            tbYear.Text = DateTime.Today.Year.ToString();
            tbSalary.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            salary.EmployeeID =Convert.ToInt32( dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            oldsalary = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbYear.Text == "")
                MessageBox.Show("Please fill the year");
            else if (cbMonth.SelectedIndex == -1)
                MessageBox.Show("Please select a month");
            else if (cbMonth.SelectedIndex == -1)
                MessageBox.Show("Please select a month");
            else if (tbSalary.Text == "")
                MessageBox.Show("Please fill the salary");
            else if (tbUser.Text == "")
                MessageBox.Show("Please select a employe from table");
            else
            {
                bool control = false;
                if (!IsUpdate)
                {
                    
                    if (salary.EmployeeID == 0)
                        MessageBox.Show("Please select an employee from table");
                    else
                    {
                        salary.Year = Convert.ToInt32(tbYear.Text);
                        salary.MonthID = Convert.ToInt32(cbMonth.SelectedValue);
                        salary.Amount = Convert.ToInt32(tbSalary.Text);
                        if (salary.Amount > oldsalary)
                            control = true;
                        SalaryBLL.AddSalary(salary,control);
                        MessageBox.Show("Salary was Added");
                        cbMonth.SelectedIndex = -1;
                        salary = new SALARY();


                    }

                }
                else
                {
                   DialogResult result = MessageBox.Show("Are you sure?","title",MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        SALARY salary = new SALARY();
                        salary.ID = detail.SalaryID;
                        salary.EmployeeID = detail.EmployeeID;
                        salary.Year = Convert.ToInt32(tbYear.Text);
                        salary.MonthID = Convert.ToInt32(cbMonth.SelectedValue);
                        salary.Amount = Convert.ToInt32(tbSalary.Text);
                        
                        if(salary.Amount> detail.OldSalary)
                            control = true;
                        SalaryBLL.UpdateSalary(salary,control);
                        MessageBox.Show("Salary was Updated");
                        this.Close();



                    
                    }



                    
                }



            }
        }
    }
}
