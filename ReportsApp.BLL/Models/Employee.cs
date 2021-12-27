using System;

namespace ReportsApp.BLL.Models
{
    public class Employee
    {
        public Guid Id { get; init; }

        public string FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? MasterName { get; set; }
    }
}
