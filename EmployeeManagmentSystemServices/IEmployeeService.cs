using EmployeeManagmentSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentSystemServices
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync(Employee emp);
        Task DeleteEmployeeAsync(Employee emp);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<Employee> GetEmployeeByNameAsync(string name);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task UpdateEmployeeAsync(Employee emp);
        Task SaveAsync();
    }
}
