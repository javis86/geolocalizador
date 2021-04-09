FROM mcr.microsoft.com/dotnet/core/sdk:5 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:5
WORKDIR /app
COPY --from=build-env /app/out .

# Run specific docker configuration
RUN cp appsettings.docker.json appsettings.json
ENTRYPOINT ["dotnet", "webapi.mongodb.dll"]