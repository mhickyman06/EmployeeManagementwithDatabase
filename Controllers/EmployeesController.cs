using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementwithdatabase1.Data;
using EmployeeManagementwithdatabase1.Models;
using EmployeeManagementwithdatabase1.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using EmployeeManagementwithdatabase1.Services;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;

namespace EmployeeManagementwithdatabase1.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeManagementDbContext _context;
        private readonly IWebHostEnvironment webHost;

        public readonly IFileManagerService FileManagerService;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(EmployeeManagementDbContext context,
            IWebHostEnvironment webHost,
            IFileManagerService fileManagerService,
            ILogger<EmployeesController> logger
            )
        {
            _context = context;
            this.webHost = webHost;
            FileManagerService = fileManagerService;
            this._logger = logger;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // throw new Exception("Error occured in this line");
            if (id == null)
            {
            
                Response.StatusCode=404;
                return View("NotFound",id.Value);
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return View("Notfound",id.Value);
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender,Email,Department,Photo")] CreateViewModel createView)
        {
            if (ModelState.IsValid)
            {
               
                    Employee NewEmployee = new Employee
                    {
                        Name = createView.Name,
                        Gender = createView.Gender,
                        Department = createView.Department,
                        Email = createView.Email,
                        PhotoPath = await FileManagerService.SaveImage(createView.Photo)
                    };
                    _context.Add(NewEmployee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details),new { Id = NewEmployee.Id });
            }
            return View(createView);
        }

        // GET: Employees/Edit/5
        public  IActionResult Edit(int? id)
        {
            _logger.LogTrace("Log Trace");
            _logger.LogDebug("Log debog");
            _logger.LogInformation("log info");
            _logger.LogWarning("log warning");
            _logger.LogError("log error");
            _logger.LogCritical("Log critical");
            if (id == null)
            {
                return NotFound();
            }

            var employee =  _context.Employees.FirstOrDefault(x=>x.Id==id);
            
            EditViewModel newemployee = new EditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                ExistingPhotoPath = employee.PhotoPath,
            };
            if (employee == null)
            {
                return NotFound();
            }
            return View(newemployee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,[Bind("Id,Name,Gender,Email,Department,Photo")] EditViewModel createView)
        {

            if (id != createView.Id)
            {
                Response.StatusCode=404;
                return View("NotFound",id.Value);
            }

            if (ModelState.IsValid)
            {
               /* try
                {*/

            Employee employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == createView.Id);


                    employee.Name = createView.Name;
                    employee.Gender = createView.Gender;
                    employee.Department = createView.Department;
                    employee.Email = createView.Email;
                    if (createView.Photo != null)
                    {
                        if (createView.ExistingPhotoPath != null)
                        {
                            FileManagerService.DeleteImage(createView);
                        }
                        employee.PhotoPath = await FileManagerService.SaveImage(createView.Photo);
                    }


                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }/*
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(createView.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }*/
            
            return View(createView);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
