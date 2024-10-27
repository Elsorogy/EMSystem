using EmployeeManagmentSystem.Domain;
using EmployeeManagmentSystemServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using ViewModels;

namespace EmployeeManagmentSystem.Controllers
{
    [Authorize(Roles ="Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IJobService jobService;
        public EmployeeController(IEmployeeService _employeeService ,IJobService _jobService)
        {
            employeeService = _employeeService;
            jobService = _jobService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> DisplayAllEmps()
        {
            try
            {
                var empList = await employeeService.GetAllEmployeesAsync();
                return View(empList);
            }
            catch (Exception ex) 
            {
                return View("Error");
            }
        }
       
        [HttpGet]
        public async Task<IActionResult> AddEmployee() 
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized();
            }
            var jobList = await jobService.GetAllJobsAsync();
            ViewBag.jobListForSelected = new SelectList(jobList, "JobId", "JobTitle");  
            return PartialView("_AddEmployee");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await employeeService.AddEmployeeAsync(employee);
                    await employeeService.SaveAsync();
                    return Json(new { isValid = true, redirectUrl = Url.Action("DisplayAllEmps") });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error occured during Adding ");
                }
            }
           
                return PartialView("_AddEmployee", employee);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var empDetails = await employeeService.GetEmployeeByIdAsync(id);
            return PartialView("_Details",empDetails);
        }
       
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var jobList = await jobService.GetAllJobsAsync();
            ViewBag.jobListForSelected = new SelectList(jobList, "JobId", "JobTitle");
            var empEdited = await employeeService.GetEmployeeByIdAsync(id);
            if (empEdited == null)
            {
              return NotFound("Data Not Found");
            }
              return PartialView("_Edit", empEdited);          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Employee empModel) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var empEdit = await employeeService.GetEmployeeByIdAsync(id);
                    empEdit.FirstName = empModel.FirstName;
                    empEdit.LastName = empModel.LastName;
                    empEdit.Email = empModel.Email;
                    empEdit.PhoneNumber = empModel.PhoneNumber;
                    empEdit.JoiningDate = empModel.JoiningDate;
                    empEdit.JobId = empModel.JobId;
                    await employeeService.UpdateEmployeeAsync(empEdit);
                    await employeeService.SaveAsync();                   
                    return RedirectToAction("DisplayAllEmps");
                }
                catch (Exception ex) 
                {
                    ModelState.AddModelError("", "Error occured during Edit ");
                }
            }          
            return PartialView("_Edit", empModel);
        }
      
        [HttpGet]
        public async Task<IActionResult> Delete(int id) 
        {
            var empDeleted = await employeeService.GetEmployeeByIdAsync(id);
            if (empDeleted != null)
            {
                return PartialView("_Delete", empDeleted);
            }
            return NotFound("This Employee Not Found");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDelete(int id)
        {
            var empDeleted = await employeeService.GetEmployeeByIdAsync(id);
            if (empDeleted != null)
            {
                try
                {
                    await employeeService.DeleteEmployeeAsync(empDeleted);
                    await employeeService.SaveAsync();
                    return RedirectToAction("DisplayAllEmps");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred. Please try again.");
                }              
            }
            return NotFound();
        }

        [AllowAnonymous]
        public async Task<IActionResult> DashBoard()
        {
            var employees = await employeeService.GetAllEmployeesAsync();
            var jobs = await jobService.GetAllJobsAsync();
            ViewBag.empNumber = employees.Count;
            ViewBag.jobNumber = jobs.Count;
            return View("DashBoard");
        }

        [AllowAnonymous]
        public async Task<IActionResult> SearchByName(string name) 
        { 
            if(name.IsNullOrEmpty())
            {
            }
            var emp = await employeeService.GetEmployeeByNameAsync(name);
            return PartialView("_SearchByName", emp);
        }
    }
}
