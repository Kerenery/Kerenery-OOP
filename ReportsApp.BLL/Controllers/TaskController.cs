using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportsApp.BLL.Interfaces;
using ReportsApp.DAL.Entities;

namespace ReportsApp.BLL.Controllers
{
    [ApiController]
    [Route("/tasks")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IEmployeeService _employeeService;
        
        public TaskController(ITaskService taskService, IEmployeeService employeeService)
        {
            _taskService = taskService;
            _employeeService = employeeService;
        }
    
        [HttpGet]
        public List<MyTask> GetAllTasks()
        {
            return _taskService.GetAllTasks();
        }
        
        [HttpGet("taskId")]
        public IActionResult GetById([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return StatusCode((int)HttpStatusCode.BadRequest);
    
            var employee = _taskService.GetById(id);
    
            if (employee == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
    
            return Ok(employee);
        }
        
        [HttpPatch]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTask([FromQuery] MyTask model)
        {
            var employeeId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var employee = await _employeeService.AsyncFindById(Guid.Parse(employeeId));
            
            if (model.Id == Guid.Empty)
                return StatusCode((int)HttpStatusCode.BadRequest);
    
            if (!ModelState.IsValid)
                return StatusCode((int)HttpStatusCode.BadRequest);
    
            MyTask taskToUpdate = new ()
            {
                Name = model.Name,
                State = model.State,
                AssignedEmployeeId = employee.Id,
                AssignedDate = DateTime.Now,
            };
            
            _taskService.UpdateTask(taskToUpdate);
            
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteTask([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return StatusCode((int)HttpStatusCode.BadRequest);
            
            if (_taskService.GetById(id) == null)
                return StatusCode((int)HttpStatusCode.NotFound);
    
            _taskService.Delete(id);
            
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromQuery] MyTask task)
        {
            var employeeId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var employee = await _employeeService.AsyncFindById(Guid.Parse(employeeId));
            
            if (!ModelState.IsValid)
                return StatusCode((int)HttpStatusCode.BadRequest);
    
            var newTask = new MyTask()
            {
                Id = Guid.NewGuid(),
                Name = task.Name,
                AssignedEmployeeId = employee.Id,
                AssignedDate = task.AssignedDate ?? DateTime.Now,
                CreationDate = DateTime.Now,
                State = 1,
            };
            
            _taskService.Create(newTask);
            return Ok();
        }
    
        [HttpGet("tasksFromTo")]
        public List<MyTask> GetTasksFromTo([FromQuery] DateTime firstDate, [FromQuery] DateTime secondDate)
        {
            return _taskService.GetTaskFromTo(firstDate, secondDate);
        }
    
        [HttpGet("tasksUnderMaster")]
        public List<MyTask> GetTasksFromSubs([FromQuery] Guid id)
        {
            return _taskService.GetSubordinatesTasks(id);
        }
    
        [HttpGet("tasksFromEmployee")]
        public List<MyTask> GetTaskFromEmployee(Guid id)
        {
            return _taskService.GetTasksByEmployee(id);
        }
    
        [HttpGet("modifiedTasks")]
        public List<MyTask> GetModifiedTasks()
        {
            return _taskService.GetModifiedTasks();
        }
    }
}