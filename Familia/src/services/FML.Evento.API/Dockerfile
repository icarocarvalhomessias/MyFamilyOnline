#EVENTOS API
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./services/FML.Evento.API/FML.Evento.API.csproj", "services/FML.Evento.API/"]
COPY ["./building blocks/FML.Core/FML.Core.csproj", "building blocks/FML.Core/"]
COPY ["./building blocks/FML.WebApi.Core/FML.WebApi.Core.csproj", "building blocks/FML.WebApi.Core/"]

RUN dotnet restore "services/FML.Evento.API/FML.Evento.API.csproj"
COPY . .
WORKDIR "services/FML.Evento.API/"
ARG BUILD_CONFIGURATION=Release
RUN dotnet build "FML.Evento.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "FML.Evento.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FML.Evento.API.dll"]
