using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;
using DAL;

namespace BLL
{
    public class TaskBLL
    {
        public static TaskDTO GetAll()
        {
            TaskDTO Taskdto = new TaskDTO();
            Taskdto.Employee = EmployeeDAO.GetEmployees();
            Taskdto.Departments = DepartmentDAO.GetDepartments();
            Taskdto.Positions = PositionDAO.GetPositions();
            Taskdto.TaskStates = TaskDAO.GetTaskStates();
            Taskdto.Task = TaskDAO.GetTasks();
            return Taskdto;
        }

        public static void AddTask(TASK task)
        {
            TaskDAO.AddTask(task);
        }

        public static void UpdateTask(TASK task)
        {
            TaskDAO.UpdateTask(task);
        }

        public static void DeleteTask(int taskStateID)
        {
            TaskDAO.DeleteTask(taskStateID);
        }

        public static void ApproveTask(int taskID, bool isAdmin)
        {
            TaskDAO.ApproveTask(taskID,isAdmin);
        }
    }
}
