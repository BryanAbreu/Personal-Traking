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
using DAL;

namespace PersonalTraking
{
    public partial class FrmTask : Form
    {
        public FrmTask()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        TaskDTO dto = new TaskDTO();
        private bool comboFull=false;
        public bool IsUpdate = false;
        public TaskDatailDTO detail = new TaskDatailDTO();

        private void FrmTask_Load(object sender, EventArgs e)
        {
            label9.Visible= false;
            cbTaskState.Visible = false;
            dto = TaskBLL.GetAll();
            dataGridView1.DataSource = dto.Employee;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "User";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible= false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible=false;
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

            cbTaskState.DataSource = dto.TaskStates;
            cbTaskState.DisplayMember = "EstateName";
            cbTaskState.ValueMember = "ID";
            cbTaskState.SelectedIndex = -1;
            if (IsUpdate)
            {
                label9.Visible = true;
                cbTaskState.Visible = true;
                tbName.Text = detail.Name;
                tbSurname.Text = detail.Surname;
                tbTitle.Text = detail.Title;
                tbContent.Text = detail.Content;
                tbUser.Text = detail.User.ToString();

                cbTaskState.DataSource = dto.TaskStates;
                cbTaskState.DisplayMember = "EstateName";
                cbTaskState.ValueMember = "ID";
                cbTaskState.SelectedValue =detail.TaskStateID;

               



            }




        }

        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                int DepartmentID = Convert.ToInt32(cbDepartment.SelectedValue);
                cbPosition.DataSource = dto.Positions.Where(x => x.DepartamentID == DepartmentID).ToList();
                List<EmployeeDetailDTO> list = dto.Employee;
                dataGridView1.DataSource = list.Where(x => x.DepartmentID == Convert.ToInt32(cbDepartment.SelectedValue)).ToList();
               
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            tbUser.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            tbName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            tbSurname.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            task.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
        }

        private void cbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
               
                List<EmployeeDetailDTO> list = dto.Employee;
                dataGridView1.DataSource = list.Where(x => x.DepartmentID == Convert.ToInt32(cbPosition.SelectedValue)).ToList();

            }
        }
       TASK task = new TASK();
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (task.EmployeeID == 0)
                MessageBox.Show("Please select an employee on table");
            else if (tbTitle.Text.Trim() == "")
                MessageBox.Show("Task Tile is Empty");
            else if (tbContent.Text.Trim() == "")
                MessageBox.Show("Task Content is Empty");
            else
            {
                if (!IsUpdate)
                {
                    task.TaskTitle = tbTitle.Text;
                    task.TaskContent = tbContent.Text;
                    task.TaskStartDate = DateTime.Today;
                    task.TaskState = 1;
                    TaskBLL.AddTask(task);
                    MessageBox.Show("Task was added");
                    tbTitle.Clear();
                    tbContent.Clear();
                    task = new TASK();

                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure", "Warning",MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        TASK Update = new TASK();
                        Update.ID = detail.TaskID;
                        if (Convert.ToInt32(tbUser.Text) != detail.User)
                            Update.EmployeeID = task.EmployeeID;
                        else
                        {
                            Update.EmployeeID = detail.EmployeeID;
                            Update.TaskTitle = detail.Title;
                            Update.TaskContent = detail.Content;
                            Update.TaskState = Convert.ToInt32(cbTaskState.SelectedValue);
                            TaskBLL.UpdateTask(Update);
                            MessageBox.Show("Task was updated");
                            this.Close();
                        
                        
                        }
                        
                       

                    
                    }

                
                
                
                }


                



            
            }

        }
    }
}
