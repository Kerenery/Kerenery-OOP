using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportsApp.BLL.Interfaces;
using ReportsApp.BLL.Mappers;
using ReportsApp.BLL.Models;
using ReportsApp.DAL.Repositories;

namespace ReportsApp.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository repository)
        {
            _employeeRepository = repository;
        }

        public Employee Create(Employee employee)
        {
            _employeeRepository.InsertEmployee(EmployeeMapper.MapEmployee(employee));
            return employee;
        }   

        public Employee? GetById(Guid id)
        {
            return _employeeRepository.GetById(id) == null
                ? null
                : EmployeeMapper.MapEmployee(_employeeRepository.GetById(id));
        }   

        public void Delete(Guid id)
        {
            _employeeRepository.DeleteEmployee(id);
        }

        public void Update(Employee entity)
        {
            _employeeRepository.UpdateEmployee(EmployeeMapper.MapEmployee(entity));
        }

        public List<Employee> GetEmployees()
        {
            return EmployeeMapper.MapEmployees(_employeeRepository.GetAllEmployees());
        }

        public Task<DAL.Entities.Employee> AsyncFindById(Guid id)
        {
            return _employeeRepository.AsyncFindById(id);
        }
    }
}