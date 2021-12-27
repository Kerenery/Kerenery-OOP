using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsApp.DAL.Entities
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string FirstName { get; init; }

        public string? SecondName { get; init; }

        public Guid? MasterId { get; init; }

        public string? MasterName { get; init; }
    }
}