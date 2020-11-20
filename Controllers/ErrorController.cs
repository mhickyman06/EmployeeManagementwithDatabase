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
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementwithdatabase1.Controllers
{
    public class ErrorController :Controller
    {
        private readonly ILogger _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            this._logger=logger;
        }
        
        [Route("/Error/{statuscode}")]
        public IActionResult HttpStatusCodeHandler(int Statuscode)
        {
            var statuscoderesult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (Statuscode)
            {
                case 404: 
                    ViewBag.ErrorMessage = "sorry the resource you requested for was not found";
                    _logger.LogWarning("404 occured in the path = "+statuscoderesult.OriginalPath+" and Querystrings = "+statuscoderesult.OriginalQueryString);
                break;mm
            }
            return View("NotFound");
        }
        [Route("/Error")]
        public IActionResult Error()
        {
            var ExceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = ExceptionDetails.Path;
            ViewBag.ExceptionMessage = ExceptionDetails.Error.Message;
            ViewBag.ExceptionStackTrace = ExceptionDetails.Error.StackTrace;
            _logger.LogError("the path "+ExceptionDetails.Path+" threw an exception "+ExceptionDetails.Error.Message);
            return View("Error");
        }
    }
}