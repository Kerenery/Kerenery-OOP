using System;
using System.Collections.Generic;
using ReportsApp.DAL.Entities;
using ReportsApp.DAL.Repositories;

namespace ReportsApp.BLL.Services
{
    public class ReportService
    {
        private readonly ReportsRepository _repository;

        public ReportService(ReportsRepository repository)
        {
            _repository = repository;
        }
        
        public Report GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void AddReport(Report report)
        {
            _repository.AddReport(report);
        }

        public void DeleteReport(Guid id)
        {
            _repository.DeleteReport(id);
        }

        public List<Report> GetAllReports()
        {
            return _repository.GetAllReports();
        }

        public void ChangeState(int state, Guid id)
        {
            _repository.ChangeState(state, id);
        }
    }
}