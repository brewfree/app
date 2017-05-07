using BrewFree.Data;
using BrewFree.Data.Models;
using BrewFree.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace BrewFree
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Cookies.ApplicationCookie.AutomaticChallenge = false; // prevent api from redirecting to login
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "BrewFree API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext applicationDbContext)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentity();

            app.UseFacebookAuthentication(new FacebookOptions
            {
                AppId = Configuration["Authentication:Facebook:AppId"],
                AppSecret = Configuration["Authentication:Facebook:AppSecret"]
            });

            app.UseTwitterAuthentication(new TwitterOptions
            {
                ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"],
                ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"]
            });

            app.UseGoogleAuthentication(new GoogleOptions
            {
                ClientId = Configuration["Authentication:Google:ClientId"],
                ClientSecret = Configuration["Authentication:Google:ClientSecret"]
            });

            app.UseMicrosoftAccountAuthentication(new MicrosoftAccountOptions
            {
                ClientId = Configuration["Authentication:Microsoft:ClientId"],
                ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"]
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });
                app.UseApplicationDbContextAutoMigration(applicationDbContext);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseMiddleware<WebApiExceptionHandler>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            app.UseStaticFiles();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "BrewFree API V1");
                x.RoutePrefix = "api/swagger/ui";
            });

            app.UseAutoMapper();
        }
    }
}
