using System;
using System.Collections.Generic;
using ReportsApp.DAL.Entities;

namespace ReportsApp.BLL.Interfaces
{
    public interface IReportService
    {
        Report GetById(Guid id);
        
        void AddReport(Report report);
        
        void DeleteReport(Guid id);
        
        List<Report> GetAllReports();

        void UpdateReport(Report report);
    }
}