using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReportsApp.BLL.Interfaces;
using ReportsApp.BLL.Services;
using Microsoft.OpenApi.Models;
using ReportsApp.BLL.Authentication;
using ReportsApp.DAL.Context;
using ReportsApp.DAL.Entities;
using ReportsApp.DAL.Repositories;

namespace ReportsApp.BLL
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
            services
                .AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reports.Server", Version = "v1" });
            });
            
            services
                .AddAuthentication("EmployeeAuthorization")
                .AddScheme<AuthenticationSchemeOptions, EmployeeAuthenticationHandler>("EmployeeAuthorization", null);

            services.AddDbContext<ReportsDbContext>(opt =>
            {
                opt.EnableSensitiveDataLogging();
                opt.UseSqlServer(Configuration.GetConnectionString("MyServer"));
            });

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ICommentariesService, CommentariesService>();
            services.AddScoped<TaskRepository>();
            services.AddScoped<CommentariesRepository>();
            services.AddScoped<EmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reports.Server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}