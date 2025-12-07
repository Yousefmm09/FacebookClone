using FacebookClone.Infrastructure.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Implementations
{
    public class FileService : IFile
    {
        private readonly IWebHostEnvironment _env;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
        //private  const long _maxsize = 5 * 1024 * 1024;
        public FileService(IWebHostEnvironment webHost)
        {
            _env = webHost;
            
        }
        public async Task<string> UploadIamge(string foldername, IFormFile file)
        {
            if(file.Length < 0|| file==null)
                throw new ArgumentException("File is empty.");
            //if (file.Length > _maxsize)
            //    throw new ArgumentException("File size exceeds the allowed limit (5 MB).");
            //extations
            var ext=Path.GetExtension(file.FileName).ToLower();
            if (!_allowedExtensions.Contains(ext))
                throw new ArgumentException("Invalid file type. Only JPG and PNG are allowed.");
            //directory
            var path=Path.Combine(_env.WebRootPath, foldername); //wwwroot/User
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            // creat name 
            var FileName =$"{Guid.NewGuid().ToString().Replace("-",string.Empty)}{ext}";
            var FullPath=Path.Combine(path,FileName);
            // Save file
            using (var stream = new FileStream(FullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return FileName.Replace("\\", "/");
        }
    }
}
