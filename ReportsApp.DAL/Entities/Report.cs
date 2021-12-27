using System;
using ReportsApp.DAL.Enum;

namespace ReportsApp.DAL.Entities
{
    public class Report
    {
        public Guid Id { get; set; }

        public string Name { get; init; }

        public DateTime CreationDate { get; init; }

        public Guid CreatorId { get; init; }

        public Guid TaskId { get; init; }

        public int State { get; set; }
    }
}