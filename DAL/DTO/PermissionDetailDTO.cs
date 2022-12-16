using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class PermissionDetailDTO
    {
        public int EmployeeID { get; set; }
        public int User { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public int DepartmentID { get; set; }
        public int PositionID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }
        public int PermissionDayAmount { get; set; }
        public string StateName { get; set; }
        public int State { get; set; }
        public string Explanation { get; set; }
        public int PermissionID { get; set; }




    }
}
