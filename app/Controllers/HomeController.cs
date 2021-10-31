using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using app.Models;

namespace app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Secret()
        {
            var secret1 = ReadFromFile("secret1.txt");
            var secret2 = ReadFromFile("secret2.txt");
            return View("Secret", new BuildSecrets { Secret1 = secret1, Secret2 = secret2 });
        }

        private static string ReadFromFile(string fileName)
        {
            var file = new FileInfo(fileName);
            if (!file.Exists)
            {
                return null;
            }
            using (var stream = file.OpenRead())
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
