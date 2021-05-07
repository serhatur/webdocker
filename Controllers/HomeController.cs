using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace WebDocker.Controllers
{
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
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
    }
}
