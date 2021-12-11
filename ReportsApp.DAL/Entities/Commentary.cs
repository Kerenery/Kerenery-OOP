using System;

namespace ReportsApp.DAL.Entities
{
    public class Commentary
    {
        public Guid Id { get; set; }
        
        public string Comment { get; init; }

        public DateTime CommentDate { get; init; }

        public Guid TaskId { get; init; }

        public Guid EmployeeId { get; init; }
    }
}