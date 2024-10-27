using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagmentSystem.Domain
{
    public class Employee
    {
        [Display(Name ="ID")]
        public int EmployeeId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
           
        [DataType(DataType.Date)]
        public DateOnly? JoiningDate { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }
        public Job? Job { get; set; }


    }
}
