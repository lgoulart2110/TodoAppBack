using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Hangfire.Models;

namespace TodoApp.Hangfire
{
    public static class HangfireModuleDependency
    {
        public static void AddHangfireServiceModule(this IServiceCollection services, IConfiguration configuration)
        {
            var hangfireConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddHangfire(x => x.UseSqlServerStorage(hangfireConnectionString));
            services.AddHangfireServer();

            GlobalConfiguration.Configuration.UseSqlServerStorage(hangfireConnectionString).WithJobExpirationTimeout(TimeSpan.FromDays(7));
        }

        public static void AddHangfireAppModule(this WebApplication app)
        {
            app.UseHangfireDashboard();
        }
    }
}
