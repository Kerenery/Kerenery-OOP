using System.Collections.Generic;
using System.Linq;
using ReportsApp.BLL.Models;
using ReportsApp.DAL.Entities;
using Employee = ReportsApp.DAL.Entities.Employee;

namespace ReportsApp.BLL.Mappers
{
    public static class EmployeeMapper
    {
        public static List<Models.Employee> MapEmployees(List<DAL.Entities.Employee> employees)
        {
            return employees.Select(emp => new Models.Employee
            {
                Id = emp.Id,
                FirstName = emp.FirstName,
                SecondName = emp.SecondName,
                MasterName = emp.MasterName,  
            }).ToList();
        }

        public static Models.Employee MapEmployee(Employee employee)
        {
            return new Models.Employee()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                SecondName = employee.SecondName,
                MasterName = employee.MasterName,
            };
        }

        public static Employee MapEmployee(Models.Employee employee)
        {
            return new Employee()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                SecondName = employee.SecondName,
                MasterName = employee.MasterName,
            };
        }
    }
}
