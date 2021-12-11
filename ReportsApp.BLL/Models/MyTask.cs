using System;

namespace ReportsApp.BLL.Models
{
    public class MyTask
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string State { get; init; }

        public DateTime CreationDate { get; init; }

        public DateTime? AssignedDate { get; init; }
    }
}
