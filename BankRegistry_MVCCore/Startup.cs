using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankRegistry_MVCCore
{
    using Microsoft.EntityFrameworkCore;
    using Repository;
    using Domain.ServiceInterfaces;
    using Service;
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Connecting to SQL
            string connectionString = @"Server=ALIKUNHUAWEI\SQLEXPRESS;Database=BankRegistry_Core;Trusted_Connection=True;";
            services.AddDbContext<BankRegistryDbContext>(d => d.UseSqlServer(connectionString), ServiceLifetime.Singleton, ServiceLifetime.Singleton);
            /*By adding double Singleton, we make both DbContext class and DbContextOptions lifetime Singleton. They are Scoped by default, that's 
            why I didn't get two DbContexts. (It was the same because of the same request)*/

            services.AddTransient<IBankService, BankService>();
            services.AddTransient<IContactPersonService, ContactPersonService>();
            services.AddTransient<IPositionService, PositionService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
