using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Strava.NetCore
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

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/signout";
            })
            .AddStrava(options =>
            {
                options.ClientId = "4575";
                options.ClientSecret = "7d04c499f827e46369691c8dff58c724f082a37b";
                // options.Scope.Add("public");
            });

            /*
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "oauth";
            })
            .AddCookie()
            .AddOAuth("oauth", options =>
            {
                options.ClientId = Configuration["ClientId"];
                options.ClientSecret = Configuration["ClientSecret"];
                options.CallbackPath = new PathString(Configuration["CallbackPage"]);

                options.AuthorizationEndpoint = Configuration["AuthorizationEndpoint"];
                options.TokenEndpoint = Configuration["TokenEndpoint"];
                options.UserInformationEndpoint = Configuration["UserInformationEndpoint"];
            });
            */
            /*
            services.AddAuthentication().
            AddOAuth(
                "strava",
                options=> AuthenticationMiddleware.SetOAuth2Options(options)
            );
            */
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            if (true)
            {
                app.UseStaticFiles();
                app.UseAuthentication();
                app.UseMvc();
            }
            else
            {
                app.UseStaticFiles();
                app.UseSpaStaticFiles();
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller}/{action=Index}/{id?}");
                });

                app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseReactDevelopmentServer(npmScript: "start");
                    }
                });
            }
        }
    }
}
