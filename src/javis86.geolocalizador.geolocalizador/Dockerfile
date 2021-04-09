FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./javis86.geolocalizador.geolocalizador/*.csproj ./javis86.geolocalizador.geolocalizador/
COPY ./javis86.Contracts/*.csproj ./javis86.Contracts/
RUN dotnet restore javis86.geolocalizador.geolocalizador/*.csproj

# Copy everything else and build
COPY ./javis86.geolocalizador.geolocalizador/ ./javis86.geolocalizador.geolocalizador/
COPY ./javis86.Contracts/ ./javis86.Contracts/
RUN dotnet publish javis86.geolocalizador.geolocalizador/*.csproj -c Release -o out

# Build runtime image
#FROM mcr.microsoft.com/dotnet/runtime:5.0
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "javis86.geolocalizador.geolocalizador.dll"]