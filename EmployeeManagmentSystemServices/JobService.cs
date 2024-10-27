using EmployeeManagmentSystem.Domain;
using EmployeeManagmentSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentSystemServices
{
    public class JobService : IJobService
    {
        private readonly IRepository<Job> repositoryJob;

        public JobService(IRepository<Job> _repositoryJob)
        {
            repositoryJob = _repositoryJob;
        }
        public async Task AddJobAsync(Job job) => await repositoryJob.CreateAsync(job);
       

        public async Task DeleteJobAsync(Job job) => await repositoryJob.DeleteAsync(job);
       

        public async Task<List<Job>> GetAllJobsAsync() => await repositoryJob.GetAllAsync();
        

        public async Task<Job> GetJobByIdAsync(int id) => await repositoryJob.GetByIdAsync(id);

        public async Task<Job> GetJobByNameAsync(string name) =>await repositoryJob.GetByNameAsync(name);
        

        public async Task SaveAsync() => await repositoryJob.SaveAsync();
       

        public async Task UpdateJobAsync(Job job) => await repositoryJob.UpdateAsync(job);

    }
}
