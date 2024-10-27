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
    public class JobRepository : IRepository<Job>
    {
        private readonly EMSContext context;

        public JobRepository(EMSContext _context)
        {
            context = _context;
        }
        public async Task CreateAsync(Job job) => await context.AddAsync(job);
        

        public async Task DeleteAsync(Job job) =>  context.Remove(job);
       

        public async Task<List<Job>> GetAllAsync() => await context.Jobs.ToListAsync();


        public async Task<Job> GetByIdAsync(int id) => await context.Jobs.FirstOrDefaultAsync(j => j.JobId == id);

        public async Task<Job> GetByNameAsync(string name) => await context.Jobs.FirstOrDefaultAsync(j => j.JobTitle == name);
       

        public async Task SaveAsync() => await context.SaveChangesAsync();
        

        public async Task UpdateAsync(Job job) => context.Update(job);
        
      
    }
}
