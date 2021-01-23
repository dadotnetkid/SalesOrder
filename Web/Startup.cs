using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Syncfusion.Licensing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Data;
using Data.Models;
using FluentValidation.AspNetCore;
using Services;
using Services.VM;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
SyncfusionLicenseProvider.RegisterLicense("MDAxQDMxMzgyZTM0MmUzMGdVTXdwditJbDRRYjQ0QXpRL2xXOTFpZk0zcWxVeTUySW5Fc0g4MDlQbWM9");
Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.


        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

            /*services.AddDbContext<ModelDb>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
            });*/
            services.AddControllersWithViews();
            services.AddRazorPages().AddViewOptions(options =>
            {
                options.HtmlHelperOptions.ClientValidationEnabled = true;
            });
            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions()
                {
                    RequiredLength = 6,
                    RequireDigit = false,
                    RequireUppercase = false,
                    RequireLowercase=false,
                    RequireNonAlphanumeric=false
                };
                options.SignIn = new SignInOptions()
                {
                    RequireConfirmedAccount = false,
                    RequireConfirmedEmail = false,
                    RequireConfirmedPhoneNumber = false,
                    
                };
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/member/login";

            });
            services.AddMvc().AddFluentValidation(options =>
            {
                /*options.RegisterValidatorsFromAssemblyContaining<SalesOrderValidator>();
                options.RegisterValidatorsFromAssemblyContaining<SalesOrderValidator>();*/
                // /**/options.RegisterValidatorsFromAssemblyContaining<AutofacService>();/**/
                options.RegisterValidatorsFromAssemblyContaining<AutofacService>();
                options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });
            services.AddHttpContextAccessor();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacService());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=SalesOrder}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
