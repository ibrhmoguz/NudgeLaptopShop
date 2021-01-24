using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nudge.LaptopShop.Api.Data;
using Nudge.LaptopShop.Api.Interfaces;
using Nudge.LaptopShop.Api.Repositories;
using Nudge.LaptopShop.Api.Services;

namespace Nudge.LaptopShop.Api
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
            services.AddHealthChecks();
            services.AddCors(options =>
            {
                options.AddPolicy("NudgeCodingChallengeOriginPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // Register EF core
            services.AddDbContext<LaptopShopContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register laptop services
            services.AddScoped<ILaptopService, LaptopService>();
            services.AddScoped<ILaptopRepository, LaptopRepository>();

            // Enable response caching
            services.AddResponseCaching();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("NudgeCodingChallengeOriginPolicy");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            // Activate response caching middleware
            app.UseResponseCaching();
        }
    }
}
