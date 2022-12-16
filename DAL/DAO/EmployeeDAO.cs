using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class EmployeeDAO : EmployeeContext
    {
        public static void AddEmployee(EMPLOYEE employee)
        {
            try
            {
                db.EMPLOYEE.InsertOnSubmit(employee);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public static List<EmployeeDetailDTO> GetEmployees()
        {
            List<EmployeeDetailDTO> employeeList = new List<EmployeeDetailDTO>();
            var list =(from e in db.EMPLOYEE 
                       join d in db.DEPARTAMENT on e.DepartamentID equals d.ID
                       join p in db.POSITION on e.PositionID equals p.ID
                       select new { 
                       User = e.UserNo,
                       Name = e.Name,
                       Surname = e.Surname,
                       EmployeeID = e.ID,
                       Pasaword = e.Password,
                       DepartmentName = d.DepartamentName,
                       PositionName = p.PositionName,
                       DepartmentID = e.DepartamentID,
                       PositionID = e.PositionID,
                       isAdmin = e.IsAdmin,
                       Salary = e.Salary,
                       ImagePath = e.ImagePath,
                       birthDay = e.BirthDay,
                       Adress = e.Adress
                       }).OrderBy(x=> x.User).ToList();

            foreach (var item in list)
            {
                EmployeeDetailDTO dto = new EmployeeDetailDTO();
                dto.Name = item.Name;
                dto.User = item.User;
                dto.Surname = item.Surname;
                dto.EmployeeID = item.EmployeeID;
                dto.Password = item.Pasaword;
                dto.DepartmentID = item.DepartmentID;
                dto.DepartmentName = item.DepartmentName;
                dto.PositionID = item.PositionID;
                dto.PositionName = item.PositionName;
                dto.isAdmin = item.isAdmin;
                dto.Salary = item.Salary;
                dto.ImagePath = item.ImagePath;
                dto.Adress = item.Adress;
                dto.BhirtDay = item.birthDay;
                employeeList.Add(dto);
            }
            return employeeList;
        }

        public static void DeleteEmployee(int employeeID)
        {
            try
            {
                EMPLOYEE emp = db.EMPLOYEE.First(x => x.ID == employeeID);
                db.EMPLOYEE.DeleteOnSubmit(emp);
                db.SubmitChanges();

                List<TASK> task = db.TASK.Where(x => x.EmployeeID == employeeID).ToList();
                db.TASK.DeleteAllOnSubmit(task);
                db.SubmitChanges();

                List<SALARY> salary = db.SALARY.Where(x => x.EmployeeID == employeeID).ToList();
                db.SALARY.DeleteAllOnSubmit(salary);
                db.SubmitChanges();

                List<PERMISSION> permission = db.PERMISSION.Where(x => x.EmployeeID == employeeID).ToList();
                db.PERMISSION.DeleteAllOnSubmit(permission);
                db.SubmitChanges();




            }
            catch (Exception  ex)
            {

                throw ex;
            }
        }

        public static void UpdateEmployee(POSITION position)
        {
            List<EMPLOYEE> list = db.EMPLOYEE.Where(x => x.PositionID == position.ID).ToList() ;
            foreach (var item in list)
            {
                item.DepartamentID = position.DepartamentID;
            }
            db.SubmitChanges();

        }

        public static void UpdateEmployee(EMPLOYEE employee)
        {
            try
            {
                EMPLOYEE emp = db.EMPLOYEE.First(x => x.ID == employee.ID);
                emp.UserNo=employee.UserNo;
                emp.Name = employee.Name;
                emp.Surname = employee.Surname;
                emp.Password = employee.Password;
                emp.IsAdmin = employee.IsAdmin;
                emp.BirthDay = employee.BirthDay;
                emp.Adress = employee.Adress;
                employee.DepartamentID = employee.DepartamentID;
                employee.PositionID = employee.PositionID;
                emp.Salary = employee.Salary;
              
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateEmployee(int employeeID, int amount)
        {
            try
            {
                EMPLOYEE employee = db.EMPLOYEE.First(x => x.ID == employeeID);
                employee.Salary = amount;
                db.SubmitChanges();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<EMPLOYEE> GetEmployees(int v, string text)
        {
            try
            {
                 List<EMPLOYEE> List = db.EMPLOYEE.Where(x => x.UserNo == v && x.Password == text).ToList();

                return List;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public static List<EMPLOYEE> GetUSer(int v)
        {
            return db.EMPLOYEE.Where(x => x.UserNo == v).ToList();
        }
    }
}
