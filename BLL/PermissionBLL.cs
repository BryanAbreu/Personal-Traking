using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DAL.DAO;
using DAL.DTO;

namespace BLL
{
    public class PermissionBLL
    {
        public static void AddPermission(PERMISSION permission)
        {
            PermissionDAO.AddPermission(permission);
        }

        public static PermissionDTO GetAll()
        {
          PermissionDTO dto = new PermissionDTO();
            dto.Departments = DepartmentDAO.GetDepartments();
            dto.Positions = PositionDAO.GetPositions();
            dto.State = PermissionDAO.GetState();
            dto.Permissions = PermissionDAO.GetPermission();
            return dto;
        }

        public static void UpdatePermission(PERMISSION permission)
        {
            PermissionDAO.UpdatePermission(permission);

        }

        public static void UpdatePermission(int permissionID, int appreved)
        {
            PermissionDAO.UpdatePermission(permissionID, appreved);
        }

        public static void DeletePermission(int permissionID)
        {
            PermissionDAO.DeletePermission(permissionID);
        }
    }
}
