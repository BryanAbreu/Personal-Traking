using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class PermissionDTO
    {
        public List<DEPARTAMENT> Departments { get; set; }
        public List<PositionDTO> Positions { get; set; }
        public List<PERMISSIONSTATE> State { get; set; }
        public List<PermissionDetailDTO> Permissions { get; set; }


    }
}
