using EmployeeManagement.Core.Mappers;
using EmployeeManagement.Core.RepositoryContracts;
using EmployeeManagement.Core.ServiceContracts;
using EmployeeManagement.Core.Services;
using EmployeeManagement.Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Core
{
    public static class DependancyInjection
    {
        /// <summary>
        /// Registers all Core layer services: AutoMapper, FluentValidation, and Employee business logic service.
        /// </summary>
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            // AutoMapper – registers all profiles in this assembly
            services.AddAutoMapper(typeof(EmployeeProfile).Assembly);

            // FluentValidation – registers all validators in this assembly
            services.AddValidatorsFromAssemblyContaining<EmployeeAddRequestValidator>();

            // Business Logic Services
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
