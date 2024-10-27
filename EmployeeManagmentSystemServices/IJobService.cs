using EmployeeManagmentSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentSystemServices
{
    public interface IJobService
    {
        Task AddJobAsync(Job job);
        Task DeleteJobAsync(Job job);
        Task<List<Job>> GetAllJobsAsync();
        Task<Job> GetJobByIdAsync(int id);
        Task<Job> GetJobByNameAsync(string name);
        Task UpdateJobAsync(Job job);
        Task SaveAsync();

    }
}
