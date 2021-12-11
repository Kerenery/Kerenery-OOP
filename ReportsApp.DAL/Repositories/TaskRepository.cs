using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportsApp.DAL.Context;
using ReportsApp.DAL.Entities;
using ReportsApp.DAL.Tools;

namespace ReportsApp.DAL.Repositories
{
    public class TaskRepository
    {
        private readonly ReportsDbContext _reportsDbContext;

        public TaskRepository(ReportsDbContext context)
        {
            _reportsDbContext = context;
        }

        public List<MyTask> GetAll()
        {
            return _reportsDbContext.Tasks.ToList();
        }

        public MyTask GetTaskById(Guid id)
        {
            return _reportsDbContext.Tasks.FirstOrDefault(task => task.Id == id);
        }

        public List<MyTask> GetTasksByCreationTime(DateTime firstDate, DateTime lastDate)
        {
            var query = from task in _reportsDbContext.Tasks
                where task.CreationDate >= firstDate & task.CreationDate <= lastDate
                orderby task.CreationDate descending
                select task;
            
            return query.ToList();
        }

        public List<MyTask> GetTasksByEmployeeId(Guid id)
        {
            var query = from task in _reportsDbContext.Tasks
                where task.AssignedEmployeeId == id
                orderby task.CreationDate descending
                select task;

            return query.ToList();
        }

        public List<MyTask> GetModifiedTasks()
        {
            var query = from task in _reportsDbContext.Tasks
                join comment in _reportsDbContext.Commentaries on task.Id equals comment.TaskId
                select task;

            return query.ToList();
        }


        public List<MyTask> GetSubordinatesTasks(Guid masterId)
        {
            var subordinates = _reportsDbContext
                .Employees
                .Where(emp => emp.MasterId == masterId);

            var query = _reportsDbContext.Tasks
                .Where(task => subordinates.Any(em => em.Id == task.AssignedEmployeeId));

            return query.ToList();
        }

        public void UpdateTask(MyTask task)
        {
            _reportsDbContext.Entry(task).State = EntityState.Modified;
            _reportsDbContext.Tasks.Update(task);
            _reportsDbContext.SaveChanges();
        }

        public void CreateTask(MyTask task)
        {
            task.Id = Guid.NewGuid();
            _reportsDbContext.Tasks.Add(task);
            _reportsDbContext.SaveChanges();
        }

        public void DeleteTask(Guid id)
        {
            var taskToDelete = _reportsDbContext.Tasks.FirstOrDefault(task => task.Id == id);
            if (taskToDelete is null)
                throw new ReportsDALException("can't delete non-existent task");

            _reportsDbContext.Tasks.Remove(taskToDelete);
            _reportsDbContext.SaveChangesAsync();
        }
    }
}