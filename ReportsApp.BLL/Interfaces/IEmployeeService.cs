using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportsApp.BLL.Models;

namespace ReportsApp.BLL.Interfaces
{
    public interface IEmployeeService
    {
        Employee Create(Employee employee);

        Employee? GetById(Guid id);

        void Delete(Guid id);

        void Update(Employee entity);

        List<Employee> GetEmployees();

        Task<DAL.Entities.Employee> AsyncFindById(Guid id);
    }
}