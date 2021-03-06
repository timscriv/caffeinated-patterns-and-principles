using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StrategyBasic.Core;
using StrategyBasic.Core.Strategies;

namespace StrategyBasic
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

            services.AddScoped<ICoffeeContext, CoffeeContext>()
                .AddScoped<DripStrategy>()
                .AddScoped<EspressoStrategy>()
                .AddScoped<FrenchPressStrategy>()
                .AddScoped<PourOverStrategy>()
                .AddScoped(c=> new Lazy<ICoffeeStrategy, BrewMethod>(() => c.GetRequiredService<DripStrategy>(), BrewMethod.Drip))
                .AddScoped(c=> new Lazy<ICoffeeStrategy, BrewMethod>(() => c.GetRequiredService<EspressoStrategy>(), BrewMethod.Espresso))
                .AddScoped(c=> new Lazy<ICoffeeStrategy, BrewMethod>(() => c.GetRequiredService<FrenchPressStrategy>(), BrewMethod.FrenchPress))
                .AddScoped(c=> new Lazy<ICoffeeStrategy, BrewMethod>(() => c.GetRequiredService<PourOverStrategy>(), BrewMethod.PourOver));
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
        }
    }
}
