using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementwithdatabase1.Models
{
    public enum Gender {
      Male,
      Female
    }
    public enum Department
    {
        LEGAL,
        PLN,
        GGM,
        MMD,
        ITD,
        GAS,
        FAD,
        GRC,
        SERV,
        JV_OPS,
    };

    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        [Required]
        public Department? Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
