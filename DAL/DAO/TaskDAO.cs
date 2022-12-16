using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class TaskDAO : EmployeeContext
    {
        public static List<TASKSTATE> GetTaskStates()
        {
            return db.TASKSTATE.ToList();
        }

        public static void AddTask(TASK task)
        {
            try
            {
                db.TASK.InsertOnSubmit(task);
                db.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<TaskDatailDTO> GetTasks()
        {
            List<TaskDatailDTO> tasklist = new List<TaskDatailDTO>();

            var list = (from t in db.TASK
                        join s in db.TASKSTATE on t.TaskState equals s.ID
                        join e in db.EMPLOYEE on t.EmployeeID equals e.ID
                        join d in db.DEPARTAMENT on e.DepartamentID equals d.ID
                        join p in db.POSITION on e.PositionID equals p.ID
                        select new
                        {
                            taskID = t.ID,
                            title = t.TaskTitle,
                            content = t.TaskContent,
                            startDate = t.TaskStartDate,
                            deliveryDate = t.TaskDeliveryDate,
                            taskstateName = s.EstateName,
                            taskstateID = t.TaskState,
                            user = e.UserNo,
                            name = e.Name,
                            employeeID = e.ID,
                            surname = e.Surname,
                            positionName = p.PositionName,
                            departmentName = d.DepartamentName,
                            positionID = e.PositionID,
                            departmentID = e.DepartamentID,



                        }).OrderBy(x => x.startDate).ToList();


            foreach (var item in list)
            {
                TaskDatailDTO dto = new TaskDatailDTO();

                dto.TaskID = item.taskID;
                dto.Title = item.title;
                dto.Content = item.content;
                dto.TaskStartDate = item.startDate;
                dto.TaskDaliverytDate = item.deliveryDate;
                dto.TaskStateName = item.taskstateName;
                dto.TaskStateID = item.taskstateID;
                dto.User = item.user;
                dto.Name = item.name;
                dto.Surname = item.surname;
                dto.PositionName = item.positionName;
                dto.DepartmentName = item.departmentName;
                dto.PositionID = item.positionID;
                dto.EmployeeID = item.employeeID;
                tasklist.Add(dto);

            }
            return tasklist;
        }

        public static void ApproveTask(int taskID, bool isAdmin)
        {
            try
            {
                TASK task = db.TASK.First(x => x.ID == taskID);
                if (isAdmin)
                    task.TaskState = TaskStates.Approved;
                else
                    task.TaskState = TaskStates.Delivery;
                task.TaskDeliveryDate = DateTime.Today;
                db.SubmitChanges();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void DeleteTask(int taskStateID)
        {
            try
            {
                TASK ts = db.TASK.First(x => x.ID == taskStateID);
                db.TASK.DeleteOnSubmit(ts);
                db.SubmitChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateTask(TASK task)
        {
            try
            {
                TASK ts = db.TASK.First(x => x.ID == task.ID);
                ts.TaskTitle = task.TaskTitle;
                ts.TaskContent = task.TaskContent;
                ts.TaskState = task.TaskState;
                ts.EmployeeID = task.EmployeeID;
                db.SubmitChanges();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
