using System;
using System.Collections.Generic;
using System.Linq;
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

        public void ChangeState(int state, Guid id)
        {
            if (state is <= 0 or > 3)
                throw new ReportsDALException("state is wrong, i can feel it");
            
            var report = _context.Reports.FirstOrDefault(r => r.Id == id)
                                 ?? throw new ReportsDALException("such report does not really exist");

            report.State = state;
            _context.SaveChanges();
        }
    }
}