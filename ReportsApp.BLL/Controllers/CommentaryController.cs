using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportsApp.BLL.Interfaces;
using ReportsApp.DAL.Entities;

namespace ReportsApp.BLL.Controllers
{
    [ApiController]
    [Route("/commentaries")]
    [Authorize]
    public class CommentaryController : ControllerBase
    {
        private readonly ICommentariesService _commentariesService;
        
        public CommentaryController(ICommentariesService commentariesService)
        {
            _commentariesService = commentariesService;
        }

        [HttpGet]
        public List<Commentary> GetAll()
        {
            return _commentariesService.GetAll();
        }

        [HttpGet("/commentaryFromUser")]
        public List<Commentary> GetCommentaryById([FromQuery] Guid id)
        {
            return _commentariesService.GetCommentsByUser(id);
        }

        [HttpGet("/commentariesFromTask")]
        public List<Commentary> GetCommentariesByTask([FromQuery] Guid id)
        {
            return _commentariesService.GetCommentsByTask(id);
        }

        [HttpPost]
        public IActionResult CreateComment([FromQuery] Commentary commentary)
        {
            if (!ModelState.IsValid)
                return StatusCode((int)HttpStatusCode.BadRequest);

            var comment = new Commentary()
            {
                Id = Guid.NewGuid(),
                Comment = commentary.Comment,
                CommentDate = DateTime.Now,
                EmployeeId = commentary.Id,
                TaskId = commentary.TaskId,
            };
            
            _commentariesService.CreateCommentary(commentary);
            return Ok();
        }
        
    }
}