FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PasteBin.UrlGeneratorService/PasteBin.UrlGeneratorService.csproj", "PasteBin.UrlGeneratorService/"]
RUN dotnet restore "./PasteBin.UrlGeneratorService/./PasteBin.UrlGeneratorService.csproj"
COPY . .
WORKDIR "/src/PasteBin.UrlGeneratorService"
RUN dotnet build "./PasteBin.UrlGeneratorService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PasteBin.UrlGeneratorService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PasteBin.UrlGeneratorService.dll"]