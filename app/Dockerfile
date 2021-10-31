FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
RUN apt update && apt upgrade -y

#Build secrets
ARG SECRET_1
ARG SECRET_2

WORKDIR /app
# Copy csproj and restore as distinct layers
COPY app.csproj .
RUN dotnet restore

## Copy everything else and build
COPY . .
RUN dotnet publish -c release -o out --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
RUN apt update && apt upgrade -y

#Build secrets
ARG SECRET_1
ARG SECRET_2

WORKDIR /app

#Save first build secret value to a file 
RUN SECRET_1_VAL=$(echo $SECRET_1|base64 -d) && \
    echo $SECRET_1_VAL>secret1.txt
RUN echo $SECRET_1|base64 -d
#Save second build secret value to a file 
RUN echo $SECRET_2|base64 -d>secret2.txt
RUN echo $SECRET_2|base64 -d

#Copy built application to the runtime environment
COPY --from=build-env /app/out .
RUN  addgroup --gid 1000  non-root && \
     adduser --gid 1000 --uid 1000 non-root --no-create-home --gecos GECOS --disabled-login
RUN chown -R non-root:non-root .
#Container running with a created non-root user with ID 1000
USER 1000

EXPOSE 8000
#require env-var ASPNETCORE_URLS=http://+:8000 - set in docker-compose.yaml or in radixconfig.yaml
ENTRYPOINT ["dotnet", "app.dll"]

