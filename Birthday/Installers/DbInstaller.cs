using Business.Interfaces;
using Business.Services;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Birthday.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BirthDayDatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BirthDatabaseContext"),
                assembly => assembly.MigrationsAssembly(typeof(BirthDayDatabaseContext).Assembly.FullName));
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            });

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped(typeof(ServiceResponse<>), typeof(ServiceResponse<>));

        }
    }
}
