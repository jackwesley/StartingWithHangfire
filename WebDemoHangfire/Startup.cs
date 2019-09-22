using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebDemoHangfire.Data;
using WebDemoHangfire.Data.Implementations;
using WebDemoHangfire.Data.Interfaces;
using WebDemoHangfire.Models;
using WebDemoHangfire.Service.Implementation;
using WebDemoHangfire.Service.Intefaces;

namespace WebDemoHangfire
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
            #region BD
            //services.AddTransient<IRepositoryBase<User>, UserRepository>();
            //services.AddDbContext<UserDbContext>(cfg =>
            //{               
            //    cfg.UseSqlServer(Configuration.GetConnectionString("UserHFConnection"));
            //});
            #endregion

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IJobToProcess, JobToProcess>();

            #region HANGFIRE
            services.AddHangfire(
                    config =>
                    {
                        config.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"));

                        // console colorido
                        config.UseColouredConsoleLogProvider();
                    });
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region HANGFIRE
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            #endregion

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
