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

namespace PersonalTraking
{
    public partial class FrmDepartment : Form
    {
        public FrmDepartment()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(tbDeparment.Text.Trim()=="") 
                MessageBox.Show("Please fill the name field");
            else
            {

              DEPARTAMENT dEPARTAMENT = new DEPARTAMENT();
                if (!IsUpdate)
                {
                    dEPARTAMENT.DepartamentName = tbDeparment.Text;
                    BLL.DepartmentBLL.AddDepartament(dEPARTAMENT);
                    MessageBox.Show("Department was added");
                    tbDeparment.Clear();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure", "Warning", MessageBoxButtons.YesNo);
                    if (DialogResult.Yes == result)
                    {
                        dEPARTAMENT.ID = detail.ID;
                        dEPARTAMENT.DepartamentName = tbDeparment.Text;
                        DepartmentBLL.UpdateDepartment(dEPARTAMENT);
                        MessageBox.Show("Departement was updated");
                        this.Close();
                    
                    }

                
                }
            
            };
            
        }
        public bool IsUpdate = false;
        public DEPARTAMENT detail = new DEPARTAMENT();
        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            if (IsUpdate)
                tbDeparment.Text = detail.DepartamentName;

        }
    }
}
