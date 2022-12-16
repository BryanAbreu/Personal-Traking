using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class PositionDAO : EmployeeContext
    {
        public static void AddPosition(POSITION position)
        {
            try
            {
                db.POSITION.InsertOnSubmit(position);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<PositionDTO> GetPositions()
        {
            try
            {
                var list = (from p in db.POSITION
                            join d in db.DEPARTAMENT on p.DepartamentID equals d.ID
                            select new { 
                            positionID=p.ID,
                            positionName=p.PositionName,
                            departmentName=d.DepartamentName,
                            departmentID= p.DepartamentID
                            
                            }).OrderBy(x=>x.positionID).ToList();

                List<PositionDTO> positionList = new List<PositionDTO>();
                foreach (var item in list)
                {
                    PositionDTO dto = new PositionDTO();
                    dto.ID = item.positionID;
                    dto.PositionName = item.positionName;
                    dto.DepartmentName = item.departmentName;
                    dto.DepartamentID = item.departmentID;
                    positionList.Add(dto);       
                }
                return positionList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void DeletePosition(int iD)
        {
            try
            {
                POSITION position = db.POSITION.First(x => x.ID == iD);
                db.POSITION.DeleteOnSubmit(position);
                db.SubmitChanges();
            }
            catch (Exception ex )
            {

                throw ex;
            }
        }

        public static void UpdatePosition(POSITION position)
        {
            try
            {
                POSITION pst = db.POSITION.First(x => x.ID == position.ID);
                pst.PositionName = position.PositionName;
                pst.DepartamentID = position.DepartamentID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
