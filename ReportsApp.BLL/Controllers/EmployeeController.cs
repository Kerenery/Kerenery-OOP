using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReportsApp.BLL.Interfaces;
using ReportsApp.DAL.Entities;
using Employee = ReportsApp.BLL.Models.Employee;

namespace ReportsApp.BLL.Controllers
{
    [ApiController]
    [Route("/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<Employee> GetAll()
        {
            return _service.GetEmployees();
        }


        [HttpGet("/getById")]
        public IActionResult GetById([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return StatusCode((int)HttpStatusCode.BadRequest);

            var employee = _service.GetById(id);

            if (employee == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            return Ok(employee);
        }

        [HttpPatch]
        // [ValidateAntiForgeryToken]
        public IActionResult UpdateEmployee([FromQuery] Employee model)
        {
            if (model.Id == Guid.Empty)
                return StatusCode((int)HttpStatusCode.BadRequest);

            if (!ModelState.IsValid)
                return StatusCode((int)HttpStatusCode.BadRequest);

            if (_service.GetById(model.Id) == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            
            Employee employeeToUpdate = new ()
            {
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                MasterName = model.MasterName
            };
            
            _service.Update(employeeToUpdate);
            
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteEmployee([FromQuery] Guid id)
        {
            
            if (id == Guid.Empty)
                return StatusCode((int)HttpStatusCode.BadRequest);
            
            if (_service.GetById(id) == null)
                return StatusCode((int)HttpStatusCode.NotFound);

            _service.Delete(id);
            
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromQuery] string firstName, [FromQuery] string secondName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return StatusCode((int)HttpStatusCode.BadRequest);

            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                SecondName = secondName,
                MasterName = "Fredi",
            };
            
            _service.Create(employee);
            return Ok();
        }   
    }
}