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
    public partial class FrmPermission : Form
    {
        public FrmPermission()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        TimeSpan permissionday;
        public bool Isupdate = false;
        public PermissionDetailDTO detail=new PermissionDetailDTO();
        private void FrmPermission_Load(object sender, EventArgs e)
        {
            tbUser.Text = UserStatic.User.ToString();
            if (Isupdate)
            {
                dpStart.Value = detail.StartDate;
                dpEnd.Value = detail.EndtDate;
                tbDayAmount.Text = detail.PermissionDayAmount.ToString();
                tbExplanation.Text = detail.Explanation.ToString();
                tbUser.Text = detail.User.ToString();

            }
        }

        private void dpStart_ValueChanged(object sender, EventArgs e)
        {
            permissionday= dpEnd.Value.Date - dpStart.Value.Date;
            tbDayAmount.Text = permissionday.TotalDays.ToString();
        }

        private void dpEnd_ValueChanged(object sender, EventArgs e)
        {
            permissionday = dpEnd.Value.Date - dpStart.Value.Date;
            tbDayAmount.Text = permissionday.TotalDays.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbDayAmount.Text.Trim() == "")
                MessageBox.Show("Please change end or start date");
            else if (Convert.ToInt32(tbDayAmount.Text) <= 0)
                MessageBox.Show("Permission day must be bigger than 0");
            else if (tbExplanation.Text.Trim() == "")
                MessageBox.Show("Explanation is empty");
            else
            { 
                PERMISSION permission = new PERMISSION();
                if (!Isupdate)
                {
                    permission.EmployeeID = UserStatic.EmployeeID;
                    permission.PermissionState = 1;
                    permission.PermissionStartDate = dpStart.Value.Date;
                    permission.PermissionEndDate = dpEnd.Value.Date;
                    permission.PermissionDay = Convert.ToInt32(tbDayAmount.Text);
                    permission.PermissionExplanation = tbExplanation.Text;
                    PermissionBLL.AddPermission(permission);
                    MessageBox.Show("Permission was Added");
                    permission = new PERMISSION();
                    dpStart.Value = DateTime.Today;
                    dpEnd.Value = DateTime.Today;
                    tbExplanation.Clear();
                    tbDayAmount.Clear();


                }
                else if (Isupdate)
                {
                    DialogResult result = MessageBox.Show("Are you sure", "Warning", MessageBoxButtons.YesNo);
                    if(result== DialogResult.Yes)
                    {
                        permission.ID = detail.PermissionID;
                        permission.PermissionExplanation= tbExplanation.Text;
                        permission.PermissionStartDate= dpStart.Value.Date;
                        permission.PermissionEndDate=dpEnd.Value.Date;
                        permission.PermissionDay = Convert.ToInt32(tbDayAmount.Text);
                        PermissionBLL.UpdatePermission(permission);
                        MessageBox.Show("Permission was updated");
                        this.Close();


                    }
                }
              

                
            
            }
        }
    }
}
