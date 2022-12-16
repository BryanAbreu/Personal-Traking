using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class SalaryDAO : EmployeeContext
    {
        public static List<MONTHS1> GetMonts()
        {
            return db.MONTHS1.ToList();
        }

        public static void AddSalary(SALARY salary)
        {
            try
            {
                db.SALARY.InsertOnSubmit(salary);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<SalaryDetailDTO> GetSalaries()
        {
            List<SalaryDetailDTO> salariesList = new List<SalaryDetailDTO>();
            var list = (from s in db.SALARY
                        join e in db.EMPLOYEE on s.EmployeeID equals e.ID
                        join m in db.MONTHS1 on s.MonthID equals m.ID
                        select new
                        {
                            user = e.UserNo,
                            name = e.Name,
                            surname = e.Surname,
                            EmployeeID = s.EmployeeID,
                            amount = s.Amount,
                            year = s.Year,
                            monthName = m.MonthName,
                            monthID = s.MonthID,
                            salaryID = s.ID,
                            departmentID = e.DepartamentID,
                            positionID = e.PositionID

                        }).OrderBy(x => x.year).ToList();

            foreach (var item in list)
            {
                SalaryDetailDTO dto = new SalaryDetailDTO();
                dto.User = item.user;
                dto.Name = item.name;
                dto.Surname = item.surname;
                dto.EmployeeID = item.EmployeeID;
                dto.SalaryAmount = item.amount;
                dto.SalaryYear = item.year;
                dto.MonthName = item.monthName;
                dto.MonthID = item.monthID;
                dto.SalaryID = item.salaryID;
                dto.DepartmentID = item.departmentID;
                dto.PositionID = item.positionID;
                dto.OldSalary = item.amount;
                salariesList.Add(dto);


            }

            return salariesList;
                   
        }

        public static void DeleteSalary(int salaryID)
        {
            try
            {
                SALARY sal = db.SALARY.First(x => x.ID == salaryID);
                db.SALARY.DeleteOnSubmit(sal);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateSalary(SALARY salary)
        {
            try
            {
                SALARY sl = db.SALARY.First(x => x.ID == salary.ID);
                sl.Amount = salary.Amount;
                sl.Year = salary.Year;
                sl.MonthID = salary.MonthID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
