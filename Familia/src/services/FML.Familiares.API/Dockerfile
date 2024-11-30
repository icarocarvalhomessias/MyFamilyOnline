#FAMILIARES API
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./services/FML.Familiares.API/FML.Familiares.API.csproj", "services/FML.Familiares.API/"]
COPY ["./building blocks/FML.Core/FML.Core.csproj", "building blocks/FML.Core/"]
COPY ["./building blocks/FML.WebApi.Core/FML.WebApi.Core.csproj", "building blocks/FML.WebApi.Core/"]

RUN dotnet restore "services/FML.Familiares.API/FML.Familiares.API.csproj"
COPY . .
WORKDIR "services/FML.Familiares.API/"
RUN dotnet build "FML.Familiares.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FML.Familiares.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FML.Familiares.API.dll"]
