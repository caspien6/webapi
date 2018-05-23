using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Webstore.Data;
using Webstore.Services;
using Webstore.Data.Models;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Webstore.Utility.SwaggerHelper;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Webstore
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
            services.AddDbContext<R0ga3cContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<R0ga3cContext>()
                .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowEverything", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                );
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IKategoryService, KategoryService>();
            services.AddTransient<ITermekService, TermekService>();
            services.AddTransient<IKosarService, KosarService>();
            services.AddTransient<IVevoService, VevoService>();
            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                });

            services.AddApiVersioning();
            services.AddSwaggerGen(c =>
            {
                c.DocInclusionPredicate((version, apiDescription) =>
                {
                    bool controller;
                    var actionApiVersionModel = apiDescription.ActionDescriptor?.GetApiVersion();
                    // would mean this action is unversioned and should be included everywhere
                    if (actionApiVersionModel == null)
                    {
                        controller =  true;
                    }
                    else if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        controller = actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == version);
                    }
                    controller = actionApiVersionModel.ImplementedApiVersions.Any(v => $"v{v.ToString()}" == version);

                    var values = apiDescription.RelativePath
                        .Split('/')
                        .Skip(2);

                    apiDescription.RelativePath = "api/" + version + "/" + string.Join("/", values);

                    var versionParameter = apiDescription.ParameterDescriptions
                        .SingleOrDefault(p => p.Name == "version");

                    if (versionParameter != null)
                        apiDescription.ParameterDescriptions.Remove(versionParameter);

                    foreach (var parameter in apiDescription.ParameterDescriptions)
                        parameter.Name = char.ToLowerInvariant(parameter.Name[0]) + parameter.Name.Substring(1);

                    return controller;
                });

                c.SwaggerDoc("v1.0", new Info { Title = "My API V1.0", Version = "v1.0" });
                c.SwaggerDoc("v2.0", new Info { Title = "My API V2.0", Version = "v2.0" });
                c.OperationFilter<SwaggerDefaultValues>();
            });
            

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseCors("AllowEverything");
            app.UseStaticFiles();
            
            app.UseAuthentication();
            
            app.UseSwagger();
            
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "My API V1.0");
                c.SwaggerEndpoint("/swagger/v2.0/swagger.json", "My API V2.0");
            });

            app.UseMvc();
        }
    }
}
