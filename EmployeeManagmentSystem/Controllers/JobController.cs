using EmployeeManagmentSystem.Domain;
using EmployeeManagmentSystemServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagmentSystem.Controllers
{
    [Authorize(Roles ="Admin")]
    public class JobController : Controller
    {
        private readonly IJobService jobService;
        public JobController(IJobService _jobService)
        {
            jobService = _jobService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> DisplayAllJobs()
        {
            try
            {
                var jobList = await jobService.GetAllJobsAsync();

                return View("DisplayAllJobs", jobList);
            }
            catch (Exception ex) 
            {
                return View("Error");
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> AddJob()
        {
            return PartialView("_AddJob");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJob(Job job)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await jobService.AddJobAsync(job);
                    await jobService.SaveAsync();
                    return Json(new { isValid = true, redirectUrl = Url.Action("DisplayAllJobs") });
                }
                catch (Exception ex) 
                {
                    ModelState.AddModelError("", "Error occured during Adding");
                }
                
            }
            return PartialView("_AddJob",job);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var jobDetails = await jobService.GetJobByIdAsync(id);
            return PartialView("_Details", jobDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var jobEdit = await jobService.GetJobByIdAsync(id);
            return PartialView("_Edit", jobEdit);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Job job, int id)
        {
            var jobEdit = await jobService.GetJobByIdAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    jobEdit.JobTitle = job.JobTitle;
                    await jobService.UpdateJobAsync(jobEdit);
                    await jobService.SaveAsync();
                    return RedirectToAction("DisplayAllJobs");
                }
                catch (Exception ex) 
                {
                    ModelState.AddModelError("", "Error occured during Edit");
                }
            }
            return PartialView("_Edit", jobEdit);
        }

        
        [HttpGet]
        public async Task<IActionResult> Delete(int id) 
        {
            var deletedJob = await jobService.GetJobByIdAsync(id);
            return PartialView("_Delete", deletedJob);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDelete(int id)
        {
            var deletedJob = await jobService.GetJobByIdAsync(id);
            if (deletedJob != null) 
            {
                try
                {
                    await jobService.DeleteJobAsync(deletedJob);
                    await jobService.SaveAsync();
                    return RedirectToAction("DisplayAllJobs");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred. Please try again.");
                }
            }
            return NotFound();
          
        }

        [AllowAnonymous]
        public async Task<IActionResult> SearchByName(string name)
        {
            if (string.IsNullOrEmpty(name)) 
            {
            }
            var job = await jobService.GetJobByNameAsync(name);
            return PartialView("_SearchByName", job);
        }
    }
}
