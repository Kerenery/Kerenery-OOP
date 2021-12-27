using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportsApp.DAL.Context;
using ReportsApp.DAL.Entities;
using ReportsApp.DAL.Tools;

namespace ReportsApp.DAL.Repositories
{
    public class EmployeeRepository
    {
        private readonly ReportsDbContext _reportsDbContext;

        public EmployeeRepository(ReportsDbContext context)
        {
            _reportsDbContext = context;
        }

        public List<Employee> GetAllEmployees()
        {
            var query = from e1 in _reportsDbContext.Employees
                        join e2 in _reportsDbContext.Employees on e1.MasterId equals e2.Id
                        where e1.MasterId != null
                        select new Employee
                        {
                            Id = e1.Id,
                            FirstName = e1.FirstName,
                            SecondName = e1.SecondName,
                            MasterId = e2.Id,
                            MasterName = e2.FirstName
                        };
            
            return query.ToList();
        }

        public Employee GetById(Guid Id)
        {
            return _reportsDbContext.Employees.FirstOrDefault(e => e.Id == Id)!;
        }

        public void DeleteEmployee(Guid id)
        {
            var employeeToDelete = _reportsDbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (employeeToDelete is null)
                throw new ReportsDALException("can't delete non-existent employee");

            _reportsDbContext.Employees.Remove(employeeToDelete);
            _reportsDbContext.SaveChangesAsync();
        }

        public Employee InsertEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _reportsDbContext.Employees.Add(employee);
            _reportsDbContext.SaveChanges();
            return employee;
        }
        
        public void UpdateEmployee(Employee employee)
        {
            _reportsDbContext.Entry(employee).State = EntityState.Modified;
            _reportsDbContext.Employees.Update(employee);
            _reportsDbContext.SaveChangesAsync();
        }

        public Task<Employee> AsyncFindById(Guid id)
        {
            return _reportsDbContext.Employees.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
