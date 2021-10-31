using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using app.Models;
using Prometheus;

namespace app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICounter _requestCounter;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _requestCounter = Metrics.CreateCounter("custom_request_counter", "Custom request counter");
        }

        public IActionResult Index()
        {
            _requestCounter.Inc(1);
            return View(_requestCounter.Value);//optionally: pass the counter value to the view to display it. Otherwise return View()
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
