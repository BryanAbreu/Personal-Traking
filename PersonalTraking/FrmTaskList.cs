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
    public partial class FrmTaskList : Form
    {
        public FrmTaskList()
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
        TaskDTO dto=new TaskDTO();
        bool comboFull = false;

        void FillAllDate()
        {
            dto = TaskBLL.GetAll();
            if (!UserStatic.IsAdmin)
                dto.Task = dto.Task.Where(x => x.EmployeeID == UserStatic.EmployeeID).ToList();
            dataGridView1.DataSource = dto.Task;

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

        }

        private void FrmTaskList_Load(object sender, EventArgs e)
        {
            //pnlFormAdmin.Hide();
            FillAllDate();
            dataGridView1.Columns[0].HeaderText = "Task Title";
            dataGridView1.Columns[1].HeaderText = "User";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].HeaderText = "Start Date";
            dataGridView1.Columns[5].HeaderText = "Delivery Date";
            dataGridView1.Columns[6].HeaderText = "Task State";
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            //MessageBox.Show(UserStatic.EmployeeID.ToString() + " " + UserStatic.User.ToString() + " " + UserStatic.IsAdmin.ToString());
            if (!UserStatic.IsAdmin)
            {
                btnNew.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                pnlFormAdmin.Hide();
                btnApprove.Text = "Delivery";

            
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmTask frm = new FrmTask();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;

            FillAllDate();
            clearFilter();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.TaskID == 0)
                MessageBox.Show("Please, select a task on table");
            else
            {
                FrmTask frm = new FrmTask();
                frm.IsUpdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;

                FillAllDate();
                clearFilter();
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

            List<TaskDatailDTO> list = dto.Task;
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
            if (rbStartDate.Checked)
                list = list.Where(x => x.TaskStartDate > Convert.ToDateTime(dpStart.Value) &&
                x.TaskStartDate < Convert.ToDateTime(dpEnd.Value)).ToList();
            if (rbDeliveryDate.Checked)
                list = list.Where(x => x.TaskDaliverytDate > Convert.ToDateTime(dpStart.Value) &&
                x.TaskDaliverytDate < Convert.ToDateTime(dpEnd.Value)).ToList();
            if(cbTaskState.SelectedIndex != -1)
                list = list.Where(x => x.TaskStateID== Convert.ToInt32(cbTaskState.SelectedValue)).ToList();

            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFilter();
        }

        private void clearFilter()
        {
            tbUser.Clear();
            tbName.Clear();
            tbSurname.Clear();
            comboFull = false;
            cbDepartment.SelectedIndex = -1;
            cbPosition.DataSource = dto.Positions;
            cbPosition.SelectedIndex = -1;
            cbTaskState.SelectedIndex = -1;
            rbDeliveryDate.Checked = false;
            rbStartDate.Checked = false;
            comboFull = true;
            dataGridView1.DataSource = dto.Task;
        }
        TaskDatailDTO detail = new TaskDatailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.Name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            detail.Surname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            detail.Title = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            detail.Content = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
            detail.User = Convert.ToInt32( dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            detail.TaskStateID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString());
            detail.TaskID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString());
            detail.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString());
            detail.TaskStartDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            detail.TaskDaliverytDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[5].Value);



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete this Task", "Warning", MessageBoxButtons.YesNo);
            if (DialogResult.Yes == result)
            {
                TaskBLL.DeleteTask(detail.TaskID);
                MessageBox.Show("Task was deleted");
                FillAllDate();
                clearFilter();

            }


        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (UserStatic.IsAdmin && detail.TaskStateID == TaskStates.OnEmployee && detail.EmployeeID != UserStatic.EmployeeID)
                MessageBox.Show("Before approve a task employee have to delivery");
            else if (UserStatic.IsAdmin && detail.TaskStateID == TaskStates.Approved)
                MessageBox.Show("This task is already approved");
            else if (!UserStatic.IsAdmin && detail.TaskStateID == TaskStates.Delivery)
                MessageBox.Show("This task i already delivered");
            else if (!UserStatic.IsAdmin && detail.TaskStateID == TaskStates.Approved)
                MessageBox.Show("This task i already approved");
            else
            {
                TaskBLL.ApproveTask(detail.TaskID, UserStatic.IsAdmin);
                MessageBox.Show("Task was Updated");
                FillAllDate();
                clearFilter();
            }



        }
    }
}
