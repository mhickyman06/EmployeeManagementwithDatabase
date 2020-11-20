using EmployeeManagementwithdatabase1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementwithdatabase1.Data
{
    public class EmployeeManagementDbContext :DbContext
    {
        public EmployeeManagementDbContext(DbContextOptions<EmployeeManagementDbContext>options):base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
