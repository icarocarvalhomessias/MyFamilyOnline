#IDENTIDADE API
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /src
EXPOSE 81
EXPOSE 444

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./services/FML.Identidade.API/FML.Identidade.API.csproj", "services/FML.Identidade.API/"]
COPY ["./building blocks/FML.WebApi.Core/FML.WebApi.Core.csproj", "building blocks/FML.WebApi.Core/"]
COPY ["./building blocks/FML.Core/FML.Core.csproj", "building blocks/FML.Core/"]

RUN dotnet restore "services/FML.Identidade.API/FML.Identidade.API.csproj"
COPY . .
WORKDIR "services/FML.Identidade.API/"
RUN dotnet build "FML.Identidade.API.csproj" -c Release -o /src/build

FROM build AS publish
RUN dotnet publish "FML.Identidade.API.csproj" -c Release -o /src/publish

FROM base AS final
WORKDIR /src
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "FML.Identidade.API.dll"]
