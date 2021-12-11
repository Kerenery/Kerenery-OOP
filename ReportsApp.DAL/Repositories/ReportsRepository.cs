using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReportsApp.DAL.Context;
using ReportsApp.DAL.Entities;
using ReportsApp.DAL.Tools;

namespace ReportsApp.DAL.Repositories
{
    public class ReportsRepository
    {
        private readonly ReportsDbContext _context;

        public ReportsRepository(ReportsDbContext context)
        {
            _context = context;
        }

        public Report GetById(Guid id)
        {
            return _context.Reports.FirstOrDefault(r => r.Id == id);
        }

        public void AddReport(Report report)
        {
            report.Id = Guid.NewGuid();
            _context.Reports.Add(report);
            _context.SaveChanges();
        }

        public void DeleteReport(Guid id)
        {
            var reportToDelete = _context.Reports.FirstOrDefault(r => r.Id == id)
                                 ?? throw new ReportsDALException("such report does not really exist");

            _context.Reports.Remove(reportToDelete);
            _context.SaveChanges();
        }

        public List<Report> GetAllReports()
        {
            return _context.Reports.ToList();
        }

        public List<Report> GetWeeklyReports()
        {
            return _context.Reports.Where(r => r.CreationDate >= DateTime.Today.AddDays(-7))
                .OrderByDescending(r => r.CreationDate).ToList();
        }

        public void UpdateReport(Report report)
        {
            _context.Entry(report).State = EntityState.Modified;
            _context.Reports.Update(report);
            _context.SaveChanges();
        }
    }
}