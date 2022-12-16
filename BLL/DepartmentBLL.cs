using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;

namespace BLL
{
    public class DepartmentBLL
    {
        public static void AddDepartament(DEPARTAMENT dEPARTAMENT)
        {
            DepartmentDAO.AddDepartment(dEPARTAMENT);
        }

        public static List<DEPARTAMENT> GetDepartments()
        {
            return DepartmentDAO.GetDepartments();
        }

        public static void UpdateDepartment(DEPARTAMENT dEPARTAMENT)
        {
            DepartmentDAO.UpdateDepartment(dEPARTAMENT);
        }

        public static void DeleteDepartment(int iD)
        {
            DepartmentDAO.DeleteDepartment(iD);
        }
    }
}
