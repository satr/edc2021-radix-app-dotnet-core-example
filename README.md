# EDC 2021 conference, Radix application example on dotnet core

Install [Docker](https://docs.docker.com/get-docker/) and [dotnet core](https://docs.microsoft.com/en-us/dotnet/core/install/)

## Prometheus metrics
[.NET client](https://github.com/prometheus-net/prometheus-net)

```
Install-Package prometheus-net
Install-Package prometheus-net.AspNetCore
```

Publish `/metrics` endpoint in `Startup.cs`
```c#
//...
using Prometheus;
    public class Startup
    {
        //...
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            //...
            app.UseEndpoints(endpoints =>
            {
                //...
                endpoints.MapMetrics();
            });
```

Add a request counter custom metric `custom_request_counter`, incremented on each load of the `Index` page of the `HomeController`
```c#
//...
using Prometheus;
    public class HomeController : Controller
    {
        //...
        private readonly ICounter _requestCounter;

        public HomeController(ILogger<HomeController> logger)
        {
            //...
            _requestCounter = Metrics.CreateCounter("custom_request_counter", "Custom request counter");
        }

        public IActionResult Index()
        {
            _requestCounter.Inc(1);
            return View();
        }
```
Verify that with locally started application endpoint `http://localhost:8000/metrics` returns the custom metric `custom_request_counter` among standard metrics. Every reload of the page `http://localhost:8000/` increments a value of the `custom_request_counter`.

Run the application within the Docker
`docker-compose up`
