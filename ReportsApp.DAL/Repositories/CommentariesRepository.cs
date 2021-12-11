using System;
using System.Collections.Generic;
using System.Linq;
using ReportsApp.DAL.Context;
using ReportsApp.DAL.Entities;
using ReportsApp.DAL.Tools;

namespace ReportsApp.DAL.Repositories
{
    public class CommentariesRepository
    {
        private readonly ReportsDbContext _context;

        public CommentariesRepository(ReportsDbContext context)
        {
            _context = context;
        }

        public List<Commentary> GetAll()
        {
            return _context.Commentaries.ToList();
        }

        public Commentary GetCommentaryById(Guid id)
        {
            return _context.Commentaries.FirstOrDefault(c => c.Id == id);
        }

        public List<Commentary> GetCommentsByUser(Guid id)
        {
            var user = _context.Employees.FirstOrDefault(u => u.Id == id) ??
                       throw new ReportsDALException("such user doesn't exist");

            var query = _context.Commentaries.Where(c => c.EmployeeId == user.Id);

            return query.ToList();
        }

        public List<Commentary> GetCommentsByTask(Guid id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id)
                       ?? throw new ReportsDALException("such task doesn't exist");

            var query = _context.Commentaries.Where(c => c.TaskId == id);

            return query.ToList();
        }

        public void DeleteCommentary(Guid id)
        {
            var commentToDelete = _context.Commentaries.FirstOrDefault(c => c.Id == id);
            if (commentToDelete is null)
                throw new ReportsDALException("can't delete non-existent task");

            _context.Commentaries.Remove(commentToDelete);
            _context.SaveChangesAsync();
        }

        public void AddCommentary(Commentary commentary)
        {
            commentary.Id = Guid.NewGuid();
            _context.Commentaries.Add(commentary);
            _context.SaveChanges();
        }
    }
}