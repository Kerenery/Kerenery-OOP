using System;
using System.Collections.Generic;
using ReportsApp.BLL.Interfaces;
using ReportsApp.DAL.Entities;
using ReportsApp.DAL.Repositories;

namespace ReportsApp.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskRepository _taskRepository;

        public TaskService(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void Create(MyTask task)
        {
            _taskRepository.CreateTask(task);
        }

        public MyTask GetById(Guid id)
        {
            return _taskRepository.GetTaskById(id);
        }

        public void Delete(Guid id)
        {
            _taskRepository.DeleteTask(id);
        }

        public void UpdateTask(MyTask task)
        {
            _taskRepository.UpdateTask(task);
        }

        public List<MyTask> GetAllTasks()
        {
            return _taskRepository.GetAll();
        }
        
        public List<MyTask> GetTaskFromTo(DateTime firstDate, DateTime lastDate)
        {
            return _taskRepository.GetTasksByCreationTime(firstDate, lastDate);
        }

        public List<MyTask> GetTasksByEmployee(Guid id)
        {
            return _taskRepository.GetTasksByEmployeeId(id);
        }

        public List<MyTask> GetModifiedTasks()
        {
            return _taskRepository.GetModifiedTasks();
        }

        public List<MyTask> GetSubordinatesTasks(Guid id)
        {
            return _taskRepository.GetSubordinatesTasks(id);
        }
    }
}