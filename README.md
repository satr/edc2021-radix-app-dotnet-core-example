# EDC 2021 conference, Radix application example on dotnet core

Install [Docker](https://docs.docker.com/get-docker/) and [dotnet core](https://docs.microsoft.com/en-us/dotnet/core/install/)

### Run the application in the Docker container
* Run `docker-compose up` (or `docker-compose up --build` to rebuild existing container layers).
* Open in a browser a link [http://localhost:8000](http://localhost:8000/).
* Navigate with a link "Show build secret" or [http://localhost:8000/Home/Secret](http://localhost:8000/Home/Secret). Text boxes show secrets, defined in the file `docker-compose.yaml` as Base64 encoded values 
    ```yaml
    args:
      - SECRET_1=U29tZSBzZWNyZXQgQWNjb3VudCBJZCAiSm9obiBEb2UiCg== # Some secret Account Id "John Doe"
      - SECRET_2=UGEyMDIyMjZ3MHJkIy49LXt9JipAKCk+Cg==             # Pa$$w0rd#.=-{}&*@()>
    ``` 
    and saved during the container build to files `secret1.txt` and `secret2.txt`
* Run `docker-compose down` to remove docker containers after its use.
