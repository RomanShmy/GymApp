using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentMigrator.Runner;
using GymApp.Migrations;

namespace GymApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connectionString = new ConnectionString(Configuration.GetConnectionString("DefaultConnection"));
            services.AddSingleton(connectionString);

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddFluentMigratorCore()
                    .ConfigureRunner( builder => 
                        builder.AddPostgres()
                        .WithGlobalConnectionString(connectionString.Value)
                        .ScanIn(typeof(object).Assembly).For.Migrations()

                    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Migrate();
        }
    }
}
