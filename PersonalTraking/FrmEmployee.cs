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
using BLL;
using DAL.DTO;
using System.IO;

namespace PersonalTraking
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isnumber(e);
        }

        private void tbSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isnumber(e);
        }

        EmployeeDTO dto = new EmployeeDTO();
        public EmployeeDetailDTO detail = new EmployeeDetailDTO();
        public bool IsUpdate = false;
        public string imagePath = " ";

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            dto = EmployeeBLL.GetAll();
            cbDepartment.DataSource = dto.Departments;
            cbDepartment.DisplayMember = "DepartamentName";
            cbDepartment.ValueMember = "ID";
            cbDepartment.SelectedIndex = -1;

            cbPosition.DataSource = dto.Positions;
            cbPosition.DisplayMember = "PositionName";
            cbPosition.ValueMember = "ID";
            cbPosition.SelectedIndex = -1;
            comboFull = true;

            if (IsUpdate)
            {
                tbName.Text = detail.Name;
                tbSurname.Text = detail.Surname;
                tbUser.Text = detail.User.ToString();
                tbImgPath.Text = detail.ImagePath.ToString();
                tbSalary.Text = detail.Salary.ToString();
                tbPassword.Text = detail.Password.ToString();
                tbUser.Text = detail.User.ToString();
                chAdmin.Checked = Convert.ToBoolean(detail.isAdmin);
                tbAdress.Text = detail.Adress.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(detail.BhirtDay);
                cbPosition.SelectedValue = detail.PositionID;
                cbDepartment.SelectedValue = detail.DepartmentID;
                imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                tbImgPath.Text = imagePath;
                pictureBox1.ImageLocation = imagePath;

                if (!UserStatic.IsAdmin)
                {
                    chAdmin.Enabled = false;
                    tbUser.Enabled = false;
                    tbSalary.Enabled = false;
                    cbDepartment.Enabled = false;
                    cbPosition.Enabled = false;
                
                }





            }
        }
        bool comboFull = false;
        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull) {
                int DepartmentID = Convert.ToInt32(cbDepartment.SelectedValue);
                cbPosition.DataSource = dto.Positions.Where(x => x.DepartamentID == DepartmentID).ToList();
            }
            
        }
        string FileName = "";
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                pictureBox1.Load(openFileDialog1.FileName);
                tbImgPath.Text=openFileDialog1.FileName;
                string Unique = Guid.NewGuid().ToString();
                FileName += Unique + openFileDialog1.SafeFileName;
            
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbUser.Text.Trim() == "")
                MessageBox.Show("User no is Empty");
            else if (tbPassword.Text.Trim() == "")
                MessageBox.Show("Password is Empty");
            else if (tbName.Text.Trim() == "")
                MessageBox.Show("Name is Empty");
            else if (tbSurname.Text.Trim() == "")
                MessageBox.Show("Surname is Empty");
            else if (tbSalary.Text.Trim() == "")
                MessageBox.Show("Salary is Empty");
            else if (cbDepartment.SelectedIndex == -1)
                MessageBox.Show("Select a Department");
            else if (cbPosition.SelectedIndex == -1)
                MessageBox.Show("Select a Position");
            else
            {
                if (!IsUpdate)
                {
                    if (!EmployeeBLL.IsUnique(Convert.ToInt32(tbUser.Text)))
                        MessageBox.Show("This user no is used by another employee please change");
                    else
                    {
                        EMPLOYEE employee = new EMPLOYEE();
                        employee.UserNo = Convert.ToInt32(tbUser.Text);
                        employee.Password = tbPassword.Text;
                        employee.IsAdmin = chAdmin.Checked;
                        employee.Name = tbName.Text;
                        employee.Surname = tbSurname.Text;
                        employee.Salary = Convert.ToInt32(tbSalary.Text);
                        employee.DepartamentID = Convert.ToInt32(cbDepartment.SelectedValue);
                        employee.PositionID = Convert.ToInt32(cbPosition.SelectedValue);
                        employee.Adress = tbAdress.Text;
                        employee.BirthDay = dateTimePicker1.Value;
                        employee.ImagePath = FileName;
                        EmployeeBLL.AddEmployee(employee);
                        if (tbImgPath.Text != "")
                        {
                          File.Copy(tbImgPath.Text, @"Images\\" + FileName);
                        }
                        
                        MessageBox.Show("Employee was added");

                        tbUser.Clear();
                        tbPassword.Clear();
                        chAdmin.Checked = false;
                        tbName.Clear();
                        tbSurname.Clear();
                        tbSalary.Clear();
                        comboFull = false;
                        cbDepartment.SelectedValue = -1;
                        cbPosition.DataSource = dto.Positions;
                        comboFull = true;
                        tbAdress.Clear();
                        tbImgPath.Clear();
                        pictureBox1.Image = null;
                        dateTimePicker1.Value = DateTime.Today;
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    { 
                        EMPLOYEE employee = new EMPLOYEE();
                        if (tbImgPath.Text != imagePath)
                        {
                            if (File.Exists(@"images\\" + detail.ImagePath))
                                File.Delete(@"images\\" + detail.ImagePath);

                            File.Copy(tbImgPath.Text,@"Images\\" + FileName);
                            employee.ImagePath = FileName;
                        }
                        else
                        
                            employee.ImagePath = detail.ImagePath;
                            employee.ID = detail.EmployeeID;
                            employee.UserNo = Convert.ToInt32(tbUser.Text);
                            employee.Name = tbName.Text;
                            employee.Surname = tbSurname.Text;
                            employee.IsAdmin = chAdmin.Checked;
                            employee.Password = tbPassword.Text;
                            employee.Adress = tbAdress.Text;
                            employee.BirthDay = dateTimePicker1.Value;
                            employee.DepartamentID = Convert.ToInt32(cbDepartment.SelectedValue);
                            employee.PositionID = Convert.ToInt32(cbPosition.SelectedValue);
                            employee.Salary = Convert.ToInt32(tbSalary.Text);
                            EmployeeBLL.UpdateEmployee(employee);
                            MessageBox.Show("Employee was upsdate");
                            this.Close();               
                    
                    }
                
                }
                
            }
        }
        bool IsUnique = false;
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (tbUser.Text.Trim() == "")
                MessageBox.Show("User no is Empty");
            else
            {
                IsUnique = EmployeeBLL.IsUnique(Convert.ToInt32(tbUser.Text));
                if (!IsUnique)
                    MessageBox.Show("This user no is used by another employee please change");
                else 
                {
                    MessageBox.Show("This user is usable");
                }
                
            }
        }
    }
}
