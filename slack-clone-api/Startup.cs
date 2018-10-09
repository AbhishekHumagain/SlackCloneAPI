using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using slack_clone_repo;
using slack_clone_service.Helper;
using slack_clone_service.Interface;
using slack_clone_service.Service;

namespace slack_clone_api
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
            services.AddTransient<IMembers, Members>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddDbContext<SlackCloneDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SlackCloneConnection"),
                                            sqlOptions => sqlOptions.MigrationsAssembly("slack-clone-repo")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            });
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddCors(options =>
            {
                options.AddPolicy("SlackCloneCorsPolicy", corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin()
                    // Apply CORS policy for any type of origin
                    .AllowAnyMethod()
                    // Apply CORS policy for any type of http methods
                    .AllowAnyHeader()
                    // Apply CORS policy for any headers
                    .AllowCredentials());
                // Apply CORS policy for all users
            });
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
                app.UseHsts();
            }

            app.UseCors("SlackCloneCorsPolicy");
            app.UseCors(builder =>
            builder.WithOrigins("http://localhost:4200"));

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "areaRoute",
                //    template: "{area:exists}/{controller=Auth}/{action=Login}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}