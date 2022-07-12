using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application._Common.Extensions;
using TodoApp.Application.Services;
using FluentValidation.AspNetCore;
using TodoApp.Application.Validators;
using TodoApp.Application._Common.Interfaces;

namespace TodoApp.Application
{
    public static class ApplicationModuleDependency
    {
        public static void AddApplicationModule(this IServiceCollection services)
        {
            services.AddMapsterAutomatically();
            services.AddFluentValidation(fv =>
            {
                fv.AutomaticValidationEnabled = false;
                fv.RegisterValidatorsFromAssemblyContaining<TodoValidator>();
            });
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<INotificationService, NotificationService>();
        }

        private static void AddMapsterAutomatically(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig();
            config.Default
                .PreserveReference(true);

            config.Scan(AppDomain.CurrentDomain.GetAssemblies());
            config.Compile();

            MapsterExtensions.Mapster = new Mapper(config);
            services.AddSingleton<IMapper>(MapsterExtensions.Mapster);
        }
    }
}
