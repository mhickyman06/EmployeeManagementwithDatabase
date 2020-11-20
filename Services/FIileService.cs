using EmployeeManagementwithdatabase1.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementwithdatabase1.Services
{
    public class FileService : IFileManagerService
    {
        public ILogger _Logger { get; }

        public FileService(IConfiguration configuration
        ,ILogger<FileService> Logger)
        {
            _imagepath = configuration["Path:Images"];
            this._Logger=Logger;

        }

        public string _imagepath { get; }

        public void DeleteImage(EditViewModel editView)
        {
            try{
            var save_path = Path.Combine(_imagepath,editView.ExistingPhotoPath);
            File.Delete(save_path);
            }catch{
                _Logger.LogInformation("Error");
            }
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            try
            {
                var save_path = Path.Combine(_imagepath);
                if (!Directory.Exists(_imagepath))
                {
                    Directory.CreateDirectory(save_path);
                }
                var filename = new Guid() + image.FileName;
                using (var filestream = new FileStream(Path.Combine(save_path, filename), FileMode.Create))
                {
                    await image.CopyToAsync(filestream);
                }
                return filename;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return "Error";
            }
        }
       
    }
}
