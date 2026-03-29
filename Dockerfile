# ─────────────────────────────────────────────────────────────────────────────
# Stage 1: Build
# ─────────────────────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files first (for layer caching)
COPY ["EmployeeManagement.sln", "./"]
COPY ["EmployeeManagement.API/EmployeeManagement.API/EmployeeManagement.API.csproj", "EmployeeManagement.API/EmployeeManagement.API/"]
COPY ["EmployeeManagement.Core/EmployeeManagement.Core.csproj", "EmployeeManagement.Core/"]
COPY ["EmployeeManagementInfrastructure/EmployeeManagement.Infrastructure.csproj", "EmployeeManagementInfrastructure/"]

# Restore packages
RUN dotnet restore "EmployeeManagement.sln"

# Copy all source
COPY . .

# Build and publish
WORKDIR "/src/EmployeeManagement.API/EmployeeManagement.API"
RUN dotnet build "EmployeeManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmployeeManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ─────────────────────────────────────────────────────────────────────────────
# Stage 2: Runtime
# ─────────────────────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Create non-root user for security
RUN addgroup --system appgroup && adduser --system appuser --ingroup appgroup
USER appuser

COPY --from=publish /app/publish .

EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "EmployeeManagement.API.dll"]
