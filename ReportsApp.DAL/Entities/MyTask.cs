using System;
using System.Collections.Generic;
using ReportsApp.DAL.Enum;

namespace ReportsApp.DAL.Entities
{
    public class MyTask
    {
        public Guid Id { get; set; }

        public string Name { get; init; }

        public int State { get; init; }

        public DateTime CreationDate { get; init; }

        public DateTime? AssignedDate { get; init; }

        public Guid? AssignedEmployeeId { get; init; }

        public Guid? ReportId { get; set; }
    }
}