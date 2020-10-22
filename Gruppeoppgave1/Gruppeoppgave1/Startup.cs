using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Gruppeoppgave1.DAL;
using Gruppeoppgave1.DAL.IRepositories;
using Gruppeoppgave1.DAL.Repositories;
using Gruppeoppgave1.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Gruppeoppgave1
{
    [ExcludeFromCodeCoverage]
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
            services.AddDbContext<BestillingContext>(options =>
                options.UseSqlite("Data source=Bestilling.db"));
            services.AddScoped<IBestillingRepository, BestillingRepository>();
            services.AddScoped<IAvgangerRepository, AvgangRepository>();
            services.AddScoped<IStasjonRepository, StasjonRepository>();
            services.AddScoped<IBrukerRepository, BrukerRepository>();
            services.AddScoped<IRuteRepository, RuteRepository>();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(1800); // Hvis bruker ikke gj?r noe p? siden innen 30 min, vil bruker automatisk bli logget ut
                options.Cookie.IsEssential = true;
            });
            services.AddDistributedMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddFile("Logs/KundeLog.txt");
            }

            app.UseRouting();

            app.UseSession();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            InitializeMigrations(app);
        }

        private static void InitializeMigrations(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                DBInit.Initialize(serviceScope);
            }
        }
    }
}
