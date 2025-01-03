# EVENTOS API DOCERFILE

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 44307


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/FML.Evento.API/FML.Evento.API.csproj", "src/services/FML.Evento.API/"]
COPY ["src/building blocks/FML.Core/FML.Core.csproj", "src/building blocks/FML.Core/"]
COPY ["src/building blocks/FML.WebApi.Core/FML.WebApi.Core.csproj", "src/building blocks/FML.WebApi.Core/"]
RUN dotnet restore "./src/services/FML.Evento.API/FML.Evento.API.csproj"
COPY . .
WORKDIR "/src/src/services/FML.Evento.API"
RUN dotnet build "./FML.Evento.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FML.Evento.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FML.Evento.API.dll"]