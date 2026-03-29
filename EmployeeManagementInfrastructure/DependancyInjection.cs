using EmployeeManagement.Core.RepositoryContracts;
using EmployeeManagement.Infrastructure.DbContext;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Infrastructure
{
    public static class DependancyInjection
    {
        /// <summary>
        /// Registers Infrastructure layer services.
        /// Reads "DatabaseProvider" from appsettings.json to select the correct EF Core provider.
        /// Supported values: "SqlServer" | "PostgreSQL" | "MySQL"
        /// </summary>
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var provider = configuration["DatabaseProvider"] ?? "SqlServer";

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                switch (provider.Trim())
                {
                    case "PostgreSQL":
                        options.UseNpgsql(
                            configuration.GetConnectionString("PostgreSQL"),
                            npgsqlOptions => npgsqlOptions.MigrationsAssembly("EmployeeManagement.Infrastructure"));
                        break;

                    case "MySQL":
                        var mysqlVersion = ServerVersion.AutoDetect(
                            configuration.GetConnectionString("MySQL"));
                        options.UseMySql(
                            configuration.GetConnectionString("MySQL"),
                            mysqlVersion,
                            mysqlOptions => mysqlOptions.MigrationsAssembly("EmployeeManagement.Infrastructure"));
                        break;

                    case "SqlServer":
                    default:
                        options.UseSqlServer(
                            configuration.GetConnectionString("SqlServer"),
                            sqlOptions => sqlOptions.MigrationsAssembly("EmployeeManagement.Infrastructure"));
                        break;
                }
            });

            // Register repository
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
