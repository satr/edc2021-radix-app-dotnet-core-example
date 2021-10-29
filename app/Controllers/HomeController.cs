using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using app.Models;
using Prometheus.Client;

namespace app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICounter _requestCounter;

        public HomeController(ILogger<HomeController> logger, IMetricFactory metricFactory)
        {
            _logger = logger;
            _requestCounter = metricFactory.CreateCounter("custom_request_counter", "Custom request counter");
        }

        public IActionResult Index()
        {
            _requestCounter.Inc(1);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
