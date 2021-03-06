# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /home/pi/dotnet/OdeToFood

# copy csproj and restore as distinct layers
COPY *.sln .
COPY OdeToFood/*.csproj ./OdeToFood/
COPY OdeToFood.Core/*.csproj ./OdeToFood.Core/
COPY OdeToFood.Data/*.csproj ./OdeToFood.Data/
RUN dotnet restore

# copy everything else and build app
COPY OdeToFood/. ./OdeToFood/
COPY OdeToFood.Core/. ./OdeToFood.Core/
COPY OdeToFood.Data/. ./OdeToFood.Data/
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "OdeToFood.dll"]
