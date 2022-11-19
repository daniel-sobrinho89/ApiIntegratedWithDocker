FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ApiIntegratedWithDocker/ApiIntegratedWithDocker.csproj", "src/ApiIntegratedWithDocker/"]
RUN dotnet restore "src/ApiIntegratedWithDocker/ApiIntegratedWithDocker.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "src/ApiIntegratedWithDocker/ApiIntegratedWithDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/ApiIntegratedWithDocker/ApiIntegratedWithDocker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiIntegratedWithDocker.dll"]