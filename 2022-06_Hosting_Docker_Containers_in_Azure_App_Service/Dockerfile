# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Copy csproj and restore as distinct layers
WORKDIR /src
COPY ./src/*.sln ./
COPY ./src/Api/*.csproj ./Api/
COPY ./src/Service/webapi-in-docker-service/*.csproj ./Service/webapi-in-docker-service/
RUN dotnet restore

# Copy everything else and build
COPY ./src ./

WORKDIR /src/Api
RUN dotnet build -c Release -o /app/build --no-restore

FROM build AS publish
WORKDIR /src/Api
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "webapi-in-docker-app-service.dll"]