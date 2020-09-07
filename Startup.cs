using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentMigrator.Runner;
using GymApp.Migrations;
using GymApp.Services.interfaces;
using GymApp.Services;
using System.Reflection;
using GymApp.Repositories.interfaces;
using GymApp.Repositories;
using System.Text.Json.Serialization;

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
            services.AddControllers().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            var connectionString = new ConnectionString(Configuration.GetConnectionString("DefaultConnection"));
            services.AddSingleton(new DataFactory(connectionString.Value));

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            services.AddFluentMigratorCore()
                    .ConfigureRunner( builder => 
                        builder.AddPostgres()
                        .WithGlobalConnectionString(connectionString.Value)
                        .ScanIn(Assembly.GetExecutingAssembly()).For.All()

                    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

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
