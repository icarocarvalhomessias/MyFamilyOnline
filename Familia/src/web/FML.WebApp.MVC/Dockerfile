#MVC
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./web/FML.WebApp.MVC/FML.WebApp.MVC.csproj", "web/FML.WebApp.MVC/"]

RUN dotnet restore "web/FML.WebApp.MVC/FML.WebApp.MVC.csproj"
COPY . .
WORKDIR "web/FML.WebApp.MVC/"
RUN dotnet build "FML.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FML.WebApp.MVC.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "FML.WebApp.MVC.dll"]
