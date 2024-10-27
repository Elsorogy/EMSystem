using EmployeeManagmentSystem.Domain;
using EmployeeManagmentSystem.Repositories;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentSystemServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> repositoryEmployee;

        public EmployeeService(IRepository<Employee> _repositoryEmployee)
        {
            repositoryEmployee = _repositoryEmployee;
        }
        public async Task AddEmployeeAsync(Employee emp) => await repositoryEmployee.CreateAsync(emp);
       

        public async Task DeleteEmployeeAsync(Employee emp) => await repositoryEmployee.DeleteAsync(emp);
        

        public async Task<List<Employee>> GetAllEmployeesAsync() => await repositoryEmployee.GetAllAsync();
        

        public async Task<Employee> GetEmployeeByIdAsync(int id) => await repositoryEmployee.GetByIdAsync(id);

        public async Task<Employee> GetEmployeeByNameAsync(string name)=> await repositoryEmployee.GetByNameAsync(name);
        

        public async Task SaveAsync() =>await repositoryEmployee.SaveAsync();
       

        public async Task UpdateEmployeeAsync(Employee emp) => await repositoryEmployee.UpdateAsync(emp);
        
    }
}
