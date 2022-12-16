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
using DAL.DTO;

namespace PersonalTraking
{
    public partial class FrmPermissionList : Form
    {
        public FrmPermissionList()
        {
            InitializeComponent();
        }

        private void tbUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isnumber(e);
        }

        private void tbDayAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isnumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmPermission frm = new FrmPermission();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            FillAllData();
            CleanFilter();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.PermissionID == 0)
                MessageBox.Show("Please select a permission on table");
            else if (detail.State == PermissionState.Appreved || detail.State == PermissionState.Disapproved)
                MessageBox.Show("You can not update any approved or disapproved permission");
            else
            {
                FrmPermission frm = new FrmPermission();
                frm.Isupdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                FillAllData();
                CleanFilter();
            }
            
            
        }
        PermissionDTO dto = new PermissionDTO();
        private bool comboFull;
        void FillAllData()
        {
            dto = PermissionBLL.GetAll();


            if (!UserStatic.IsAdmin)
                dto.Permissions = dto.Permissions.Where(x => x.EmployeeID == UserStatic.EmployeeID).ToList();



            dataGridView1.DataSource = dto.Permissions;

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

            cbState.DataSource = dto.State;
            cbState.DisplayMember = "StateName";
            cbState.ValueMember = "ID";
            cbState.SelectedIndex = -1;
        }
        private void FrmPermissionList_Load(object sender, EventArgs e)
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
            dataGridView1.Columns[8].HeaderText = "Start Date";
            dataGridView1.Columns[9].HeaderText = "End Date";
            dataGridView1.Columns[10].HeaderText = "Day Amount";
            dataGridView1.Columns[11].HeaderText = "State";
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;

            if (!UserStatic.IsAdmin)
            {
                pnlForAdmin.Visible = false;
                btnApprove.Hide();
                btnDisApproved.Hide();
                btnDelete.Hide();

            }
               







        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            List<PermissionDetailDTO> list = dto.Permissions;
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
            if(rbStartDate.Checked)
                list = list.Where(x => x.StartDate < Convert.ToDateTime(dpEnd.Value) &&
                x.StartDate > Convert.ToDateTime(dpStart.Value)).ToList();
            else if(rbEndDate.Checked)
                list = list.Where(x => x.EndtDate < Convert.ToDateTime(dpEnd.Value) &&
                x.EndtDate > Convert.ToDateTime(dpStart.Value)).ToList();
            if (cbState.SelectedIndex != -1)
                list = list.Where(x => x.State == Convert.ToInt32(cbState.SelectedValue)).ToList();
            if (tbDayAmount.Text.Trim() != "")
                list = list.Where(x => x.PermissionDayAmount == Convert.ToInt32(tbDayAmount.Text)).ToList();
            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanFilter();
        }

        private void CleanFilter()
        {

            tbUser.Clear();
            tbName.Clear();
            tbSurname.Clear();
            comboFull = false;
            cbDepartment.SelectedIndex = -1;
            cbPosition.DataSource = dto.Positions;
            cbPosition.SelectedIndex = -1;
            cbState.SelectedIndex = -1;
            rbEndDate.Checked = false;
            rbStartDate.Checked = false;
            comboFull = true;
            tbDayAmount.Clear();

            dataGridView1.DataSource = dto.Permissions;
        }

        PermissionDetailDTO detail = new PermissionDetailDTO(); 
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.PermissionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[14].Value);
            detail.StartDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
            detail.EndtDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detail.Explanation = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
            detail.User = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detail.State = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
            detail.PermissionDayAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            PermissionBLL.UpdatePermission(detail.PermissionID, PermissionState.Appreved);
            MessageBox.Show("Approved");
            FillAllData();
            CleanFilter();

        }

        private void btnDisApproved_Click(object sender, EventArgs e)
        {
            PermissionBLL.UpdatePermission(detail.PermissionID, PermissionState.Disapproved);
            MessageBox.Show("Disapproved");
            FillAllData();
            CleanFilter();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete this permission", "Warning", MessageBoxButtons.YesNo);
            if (DialogResult.Yes == result)
                if (detail.State == PermissionState.Appreved || detail.State == PermissionState.Disapproved)
                    MessageBox.Show("You cannont delete approved or diaspproved permission ");
                else
                {
                    PermissionBLL.DeletePermission(detail.PermissionID);
                    MessageBox.Show("Permission was delete");
                    FillAllData();
                    CleanFilter();

                
                }



        }
    }
}
