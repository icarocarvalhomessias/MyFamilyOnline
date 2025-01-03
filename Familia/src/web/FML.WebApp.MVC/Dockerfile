# WEB APP DOCKERFILE

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["web/FML.WebApp.MVC/FML.WebApp.MVC.csproj", "web/FML.WebApp.MVC/"]
COPY ["building blocks/FML.Core/FML.Core.csproj", "building blocks/FML.Core/"]
COPY ["building blocks/FML.WebApi.Core/FML.WebApi.Core.csproj", "building blocks/FML.WebApi.Core/"]
RUN dotnet restore "web/FML.WebApp.MVC/FML.WebApp.MVC.csproj"
COPY . .
WORKDIR "/src/web/FML.WebApp.MVC"
RUN dotnet build "FML.WebApp.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FML.WebApp.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FML.WebApp.MVC.dll"]
