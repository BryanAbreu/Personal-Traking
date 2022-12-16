using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class PermissionDAO : EmployeeContext
    {
        public static void AddPermission(PERMISSION permission)
        {
            try
            {
                db.PERMISSION.InsertOnSubmit(permission);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<PERMISSIONSTATE> GetState()
        {
            return db.PERMISSIONSTATE.ToList();
        }

        public static List<PermissionDetailDTO> GetPermission()
        {
            List<PermissionDetailDTO> permissions = new List<PermissionDetailDTO>();

            var list = (from p in db.PERMISSION
                        join s in db.PERMISSIONSTATE on p.PermissionState equals s.ID
                        join e in db.EMPLOYEE on p.EmployeeID equals e.ID
                        select new
                        {
                            user = e.UserNo,
                            name = e.Name,
                            surname = e.Surname,
                            StateName = s.StateName,
                            StateID = p.PermissionState,
                            startdate = p.PermissionStartDate,
                            endDate = p.PermissionEndDate,
                            employeeID = p.EmployeeID,
                            permissionID = p.ID,
                            explanation = p.PermissionExplanation,
                            DayAmount = p.PermissionDay,
                            DepartmentID = e.DepartamentID,
                            positionID = e.PositionID,
                        }).OrderBy(x=> x.startdate).ToList();

            foreach (var item in list)
            {
                PermissionDetailDTO dto = new PermissionDetailDTO();
                dto.User = item.user;
                dto.Name = item.name;
                dto.Surname = item.surname;
                dto.EmployeeID = item.employeeID;
                dto.PermissionDayAmount = item.DayAmount;
                dto.StartDate = item.startdate;
                dto.EndtDate = item.endDate;
                dto.DepartmentID = item.DepartmentID;
                dto.PositionID = item.positionID;
                dto.State = item.StateID;
                dto.StateName = item.StateName;
                dto.Explanation = item.explanation;
                dto.PermissionID = item.permissionID;
                permissions.Add(dto);

            }
            return permissions;
        }

        public static void DeletePermission(int permissionID)
        {
            PERMISSION pr = db.PERMISSION.First(x => x.ID == permissionID);
            db.PERMISSION.DeleteOnSubmit(pr);
            db.SubmitChanges();

        }

        public static void UpdatePermission(int permissionID, int appreved)
        {
            try
            {
                PERMISSION pr = db.PERMISSION.First(x => x.ID == permissionID);
                pr.PermissionState = appreved;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdatePermission(PERMISSION permission)
        {
            try
            {
                PERMISSION pr = db.PERMISSION.First(x => x.ID == permission.ID);
                pr.PermissionStartDate = permission.PermissionStartDate;
                pr.PermissionEndDate = permission.PermissionEndDate;
              //  pr.PermissionState = permission.PermissionState;
                pr.PermissionDay = permission.PermissionDay;
                pr.PermissionExplanation = permission.PermissionExplanation;
                db.SubmitChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
