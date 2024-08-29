FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PasteBin.TextAPI/PasteBin.TextAPI.csproj", "PasteBin.TextAPI/"]
RUN dotnet restore "./PasteBin.TextAPI/./PasteBin.TextAPI.csproj"
COPY . .
WORKDIR "/src/PasteBin.TextAPI"
RUN dotnet build "./PasteBin.TextAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PasteBin.TextAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PasteBin.TextAPI.dll"]