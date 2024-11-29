#IDENTIDADE API
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./services/FML.Identidade.API/FML.Identidade.API.csproj", "services/FML.Identidade.API/"]
COPY ["./building blocks/FML.Core/FML.Core.csproj", "building blocks/FML.Core/"]
COPY ["./building blocks/FML.WebApi.Core/FML.WebApi.Core.csproj", "building blocks/FML.WebApi.Core/"]

RUN dotnet restore "services/FML.Identidade.API/FML.Identidade.API.csproj"
COPY . .
WORKDIR "services/FML.Identidade.API/"
ARG BUILD_CONFIGURATION=Release
RUN dotnet build "FML.Identidade.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "FML.Identidade.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FML.Identidade.API.dll"]
