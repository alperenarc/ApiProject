﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnewAPIproject.MessageHandlers;
using AnewAPIproject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AnewAPIproject
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connection = @"Server=mssql08.turhost.com; Initial Catalog=Api; User ID=Alperen; Password=alparc817ismail.";
            //var connection = @"Server=(localdb)\mssqllocaldb;Database=ANEWAPI;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));
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

            app.UseHttpsRedirection();
            app.UseAPIKeyMessageHandlerMiddleware();
            app.UseMvc();
            app.UseMvc(routes =>
            {
                routes
                    .MapRoute(name: "api",template: "api/{controller=User}/{action=GetUsers}/{id:int?}");
            });

        }
    }
}
