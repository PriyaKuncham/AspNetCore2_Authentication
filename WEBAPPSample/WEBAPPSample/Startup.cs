using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WEBAPPSample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using WEBAPPSample.Security.Authorization.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace WEBAPPSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // Set up configuration sources.
            //var builder = new ConfigurationBuilder()
            //     .SetBasePath(env.ContentRootPath)
            //     .AddJsonFile("appsettings.json");

            ////builder.AddEnvironmentVariables();
            //Configuration = builder.Build();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IConfiguration>(Configuration);
            //services.Configure<ConnectionString>(Configuration.GetSection("ConnectionString"));
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();
           // services.AddMvc();

            services.AddAuthentication("WEBAPPSample") //CookieAuthenticationDefaults.AuthenticationScheme
                    .AddCookie("WEBAPPSample", options =>
                    {
                        options.AccessDeniedPath = new PathString("/Security/Access");
                        options.LoginPath = new PathString("/Security/Login");
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Authenticated",
                    policy => policy.RequireAuthenticatedUser());

                options.AddPolicy("Member",
                    policy => policy.RequireClaim("MembershipId"));

                options.AddPolicy("PaidMember",
                    policy => policy.RequireClaim("HasCreditCard", "Y"));

                options.AddPolicy("Over18",
                    policy => policy.Requirements.Add(new AgeRequirement(18)));

                options.AddPolicy("CanRentNewRelease",
                    policy => policy.Requirements.Add(new RentNewReleaseRequirement()));
            });

            services.AddScoped<IAuthorizationHandler, AgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, RentNewReleaseRequirementHandler>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter("Authenticated"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            //app.UseStaticFiles();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
