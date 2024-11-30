#MVC
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./web/FML.WebApp.MVC/FML.WebApp.MVC.csproj", "web/FML.WebApp.MVC/"]
COPY ["./building blocks/FML.Core/FML.Core.csproj", "building blocks/FML.Core/"]
COPY ["./building blocks/FML.WebApi.Core/FML.WebApi.Core.csproj", "building blocks/FML.WebApi.Core/"]

RUN dotnet restore "web/FML.WebApp.MVC/FML.WebApp.MVC.csproj"
COPY . .
WORKDIR "web/FML.WebApp.MVC/"
RUN dotnet build "FML.WebApp.MVC.csproj" -c Release -o /src/build

FROM build AS publish
RUN dotnet publish "FML.WebApp.MVC.csproj" -c Release -o /src/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "FML.WebApp.MVC.dll"]
