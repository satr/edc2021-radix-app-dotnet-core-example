# EDC 2021 conference, Radix application example on dotnet core

Install [Docker](https://docs.docker.com/get-docker/) and [dotnet core](https://docs.microsoft.com/en-us/dotnet/core/install/)

## Add Prometheus metrics to the application  

### Prometheus client
[.NET client](https://github.com/prometheus-net/prometheus-net)

### Add custom metrics to the application
* Install Prometheus client packages
```
Install-Package prometheus-net
Install-Package prometheus-net.AspNetCore
```
* Publish `/metrics` endpoint in `Startup.cs`
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
* Add a request counter custom metric `custom_request_counter`, incremented on each load of the `Index` page of the `HomeController`
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

### Verify metrics
* Run the application locally and open in a browser a link [http://localhost:8000/metrics](http://localhost:8000/metrics) - it returns the custom metric `custom_request_counter` among standard metrics.
* Reload few times the index page [http://localhost:8000/](http://localhost:8000/) - this increments a value of the `custom_request_counter`.

### Run the application in the Docker container
* Run `docker-compose up` (or `docker-compose up --build` to rebuild existing container layers).
* Open in a browser a link [http://localhost:8000](http://localhost:8000/), reload the page ew times.
* Open in a browser a link [http://localhost:8000/metrics](http://localhost:8000/metrics) to see published metrics.
* Run `docker-compose down` to remove docker containers after its use.
