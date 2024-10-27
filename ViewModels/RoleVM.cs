using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class RoleVM
    {

        [Display(Name =("Role Name"))]
        public string RoleName {get; set;}
    }
}
