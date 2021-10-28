FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app
# Copy csproj and restore as distinct layers
COPY ./app/radix-app-dotnet-core-example.csproj .
RUN dotnet restore

## Copy everything else and build
COPY ./app/ .
RUN dotnet publish -c release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
RUN  addgroup --gid 1000  non-root && \
     adduser --gid 1000 --uid 1000 non-root --no-create-home --gecos GECOS --disabled-login
RUN chown -R non-root:non-root .
USER 1000
EXPOSE 8000
ENTRYPOINT ["dotnet", "radix-app-dotnet-core-example.dll"]