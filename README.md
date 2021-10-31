# EDC 2021 conference, Radix application example on dotnet core

Install [Docker](https://docs.docker.com/get-docker/) and [dotnet core](https://docs.microsoft.com/en-us/dotnet/core/install/)

* [Basic application](https://github.com/satr/edc2021-radix-app-dotnet-core-example/tree/basic)
* [Application with Prometheus custom metrics](https://github.com/satr/edc2021-radix-app-dotnet-core-example/tree/custom-metrics)

### Run the application locally
* Run the application locally and open in a browser a link [http://localhost:8000](http://localhost:8000).

### Run the application in the Docker container
* Run `docker-compose up` (or `docker-compose up --build` to rebuild existing container layers).
* Open in a browser a link [http://localhost:8000](http://localhost:8000/).
* Run `docker-compose down` to remove docker containers after its use.
