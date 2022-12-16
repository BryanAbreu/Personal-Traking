using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class DepartmentDAO : EmployeeContext
    {
        public static void AddDepartment(DEPARTAMENT dEPARTAMENT)
        {
            try
            {
                db.DEPARTAMENT.InsertOnSubmit(dEPARTAMENT);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public static List<DEPARTAMENT> GetDepartments()
        {
          return  db.DEPARTAMENT.ToList();
        }

        public static void UpdateDepartment(DEPARTAMENT dEPARTAMENT)
        {
            try
            {
                DEPARTAMENT dpt = db.DEPARTAMENT.First(x => x.ID == dEPARTAMENT.ID);
                dpt.DepartamentName = dEPARTAMENT.DepartamentName;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void DeleteDepartment(int iD)
        {
            DEPARTAMENT dpt = db.DEPARTAMENT.First(x => x.ID == iD);
            db.DEPARTAMENT.DeleteOnSubmit(dpt);
            db.SubmitChanges();
        }
    }
}
