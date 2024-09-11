using HealthMonitor.Domain.AggregatesModel;
using HealthMonitor.Infrastructure;
using HealthMonitor.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthMonitor.API.Infrastructure
{
    public static class CustomExtensionsMethods
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Pooling is disabled because of the following error:
            // Unhandled exception. System.InvalidOperationException:
            if (configuration.GetConnectionString("DefaultConnection") is string connectionString)
            {
                // The DbContext of type 'OrderingContext' cannot be pooled because it does not have a public constructor accepting a single parameter of type DbContextOptions or has more than one constructor.
                services.AddDbContext<HealthMonitorContext>(options =>
                {
                    options.UseNpgsql(connectionString);
                });
            }
            else
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            // Register the command validators for the validator behavior (validators based on FluentValidation library)
            /*services.AddSingleton<IValidator<CancelOrderCommand>, CancelOrderCommandValidator>();
            services.AddSingleton<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
            services.AddSingleton<IValidator<IdentifiedCommand<CreateOrderCommand, bool>>, IdentifiedCommandValidator>();
            services.AddSingleton<IValidator<ShipOrderCommand>, ShipOrderCommandValidator>();

            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<IBuyerRepository, BuyerRepository>();*/
            services.AddScoped<IPatientRepository, PatientRepository>();

            return services;
        }
    }
}
