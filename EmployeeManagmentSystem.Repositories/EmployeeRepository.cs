using EmployeeManagmentSystem.Data;
using EmployeeManagmentSystem.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentSystem.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly EMSContext context;

        public EmployeeRepository(EMSContext _context)
        {
            context = _context;
        }
        public async Task CreateAsync(Employee emp) => await context.AddAsync(emp);
       

        public async Task DeleteAsync(Employee emp) =>  context.Remove(emp);
        

        public async Task<List<Employee>> GetAllAsync() => await context.Employees.ToListAsync();
       

        public async Task<Employee> GetByIdAsync(int id) => await context.Employees.Include(e=>e.Job).FirstOrDefaultAsync(e=>e.EmployeeId == id);

        public async Task<Employee> GetByNameAsync(string name)=> await context.Employees.FirstOrDefaultAsync(e=>e.FirstName == name);
       

        public async Task SaveAsync()=> await context.SaveChangesAsync();
       

        public async Task UpdateAsync(Employee emp) => context.Update(emp);
        
    }
}
