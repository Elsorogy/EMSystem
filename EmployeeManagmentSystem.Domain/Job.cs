using System.ComponentModel.DataAnnotations;

namespace EmployeeManagmentSystem.Domain
{
    public class Job
    {
        [Display(Name ="ID")]
        public int JobId { get; set; }

        [Display(Name ="Job Title")]
        public string JobTitle { get; set; }

        public List<Employee>? Employees { get; set; }
    }
}
