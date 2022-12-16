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

namespace PersonalTraking
{
    public partial class FrmPosition : Form
    {
        public FrmPosition()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        List<DEPARTAMENT> DepartmentList   = new List<DEPARTAMENT>();
        public bool IsUpdate = false;
        public PositionDTO detail = new PositionDTO();

        private void FrmPosition_Load(object sender, EventArgs e)
        {
            DepartmentList = DepartmentBLL.GetDepartments();
           
            cbDepartment.DataSource = DepartmentList;
            cbDepartment.DisplayMember = "DepartamentName";
            cbDepartment.ValueMember = "ID";
            cbDepartment.SelectedIndex = -1;

            if (IsUpdate)
            {
                tbPosition.Text = detail.PositionName;
                cbDepartment.SelectedValue = detail.DepartamentID;

            
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbPosition.Text.Trim() == "")
                MessageBox.Show("Please fill the name field");

            else if (cbDepartment.SelectedIndex == -1)
                MessageBox.Show("Please select a department");

            else
            {
                if (!IsUpdate)
                {
                    POSITION position = new POSITION();
                    position.PositionName = tbPosition.Text;
                    position.DepartamentID = Convert.ToInt32(cbDepartment.SelectedValue);
                    BLL.PositionBLL.AddPosition(position);
                    MessageBox.Show("Position was added");
                    tbPosition.Clear();
                    cbDepartment.SelectedIndex = -1;
                }
                else
                { 
                    POSITION position =new POSITION();
                    position.ID = detail.ID;
                    position.PositionName = tbPosition.Text;
                    position.DepartamentID = Convert.ToInt32(cbDepartment.SelectedValue);
                    bool control = false;
                    if (Convert.ToInt32(cbDepartment.SelectedValue) != detail.OldDepartementID)
                        control = true;
                    PositionBLL.UpdatePosition(position,control);
                    MessageBox.Show("Position was Updated");
                    this.Close();

                
                }

            };
        }
    }
}
