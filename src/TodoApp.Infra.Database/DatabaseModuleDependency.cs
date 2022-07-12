using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Domain._Common.Adapters;
using TodoApp.Domain.Adapters;
using TodoApp.Infra.Database._Common.Persistence;
using TodoApp.Infra.Database.Context;
using TodoApp.Infra.Database.Repositories;

namespace TodoApp.Infra.Database
{
    public static class DatabaseModuleDependency
    {
        public static void AddDatabaseModule(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TodoContext>(options => options.UseSqlServer(databaseConnectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITodoRepository, TodoRepository>();
        }
    }
}
