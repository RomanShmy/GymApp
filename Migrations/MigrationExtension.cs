using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GymApp.Migrations
{
    public static class MigrationExtension
    {
        public static IApplicationBuilder Migrate(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
            runner.ListMigrations();
            runner.MigrateUp(20200902);
            return app;
        }
    }
}