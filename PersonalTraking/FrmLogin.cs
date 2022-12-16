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
using DAL.DAO;


namespace PersonalTraking
{
    public partial class Frmlogin : Form
    {
        public Frmlogin()
        {
            InitializeComponent();
        }

        private void Frmlogin_Load(object sender, EventArgs e)
        {


        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tbUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isnumber(e);
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            
            if (tbUser.Text == "" || tbPassword.Text == "")
                MessageBox.Show("Please fill the user and password");
            else
            {
                List<EMPLOYEE> employeeList = EmployeeBLL.GetEmployee(Convert.ToInt32(tbUser.Text), tbPassword.Text);
                if (employeeList.Count == 0)
                    MessageBox.Show("Please control your Information");
                else
                {

                    EMPLOYEE employee = new EMPLOYEE();
                    employee = employeeList.First();
                    UserStatic.EmployeeID = employee.ID;
                    UserStatic.User = employee.UserNo;
                    UserStatic.IsAdmin = Convert.ToBoolean( employee.IsAdmin);
                    FrmMain frmMain = new FrmMain();
                    this.Hide();
                    frmMain.ShowDialog();

                }
                
            }



        }
    }
}
