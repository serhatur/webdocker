using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebDocker.Controllers
{
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IFileProvider _fileProvider;

        public HomeController(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        [HttpGet]
        public Task<string> Index()
        {
            var zone = TimeZoneInfo.Local;
            var str = zone.DisplayName;
            str += $"\n{DateTime.Now:dd.MM.yyyy HH:mm:ss:fff}";
            str += $"\n{CultureInfo.CurrentCulture.Name}";
            str += $"\n{DateTime.Now.ToString()}";
            return Task.FromResult(str);
        }

        [HttpPost]
        public async Task<string> SaveImage(IFormFile formFile)
        {
            if(formFile != null && formFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(),"images",fileName);

                using (var stream  = new FileStream(path,FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            return formFile.FileName;
        }

        [HttpGet]
        public IEnumerable<string> ShowImage()
        {
            var images = _fileProvider.GetDirectoryContents("images").ToList().Select(x => x.Name);
            return images;
        }
    }
}
