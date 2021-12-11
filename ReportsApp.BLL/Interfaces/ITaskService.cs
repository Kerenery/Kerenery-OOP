using System;
using System.Collections.Generic;
using ReportsApp.DAL.Entities;

namespace ReportsApp.BLL.Interfaces
{
    public interface ITaskService
    {
        void Create(MyTask task);
        
        MyTask GetById(Guid id);

        void Delete(Guid id);

        void UpdateTask(MyTask task);

        List<MyTask> GetAllTasks();

        List<MyTask> GetTaskFromTo(DateTime firstDate, DateTime lastDate);

        List<MyTask> GetTasksByEmployee(Guid id);

        List<MyTask> GetModifiedTasks();

        List<MyTask> GetSubordinatesTasks(Guid id);
    }
}