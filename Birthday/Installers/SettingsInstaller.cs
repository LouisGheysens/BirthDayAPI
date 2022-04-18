using HelperLibrary;

namespace Birthday.Installers
{
    public class SettingsInstaller: IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var configSettings = configuration.GetSection("ConnectionStrings");
            services.Configure<ConfigSettings>(configSettings);
        }
    }
}
