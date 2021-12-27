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
    [Route("/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IEmployeeService _employeeService;
        
        public ReportController(IReportService reportService, IEmployeeService employeeService)
        {
            _reportService = reportService;
            _employeeService = employeeService;
        }
        
        [HttpGet("reportId")]
        public IActionResult GetById([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return StatusCode((int)HttpStatusCode.BadRequest);
    
            var employee = _reportService.GetById(id);
    
            if (employee == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
    
            return Ok(employee);
        }
        
        [HttpPatch]
        public async Task<IActionResult> UpdateReport([FromQuery] Guid taskId, [FromQuery] string name, [FromQuery] int state)
        {
            var employeeId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var employee = await _employeeService.AsyncFindById(Guid.Parse(employeeId));
            
            if (taskId == Guid.Empty)
                return StatusCode((int)HttpStatusCode.BadRequest);

            var report = new Report()
            {
                CreatorId = employee.Id,
                Name = name,
                State = state,
            };
            
            _reportService.UpdateReport(report);
            
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromQuery] Guid taskId, [FromQuery] string name, [FromQuery] int state)
        {
            var employeeId = User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var employee = await _employeeService.AsyncFindById(Guid.Parse(employeeId));

            if (taskId == Guid.Empty)
                return StatusCode((int)HttpStatusCode.BadRequest);
            
            var report = new Report()
            {
                CreatorId = employee.Id,
                Name = name,
                State = state,
                CreationDate = DateTime.Now,
                TaskId = taskId,
            };
            
            _reportService.AddReport(report);

            return Ok();
        }
        
        
        [HttpGet]
        public List<Report> GetAllReports()
        {
            return _reportService.GetAllReports();
        }
    }
}