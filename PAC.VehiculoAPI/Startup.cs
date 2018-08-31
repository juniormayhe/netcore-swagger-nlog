using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog.Web;
using PAC.Business;
using PAC.Common.Extensions;
using PAC.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace PAC.VehiculoAPI
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _hostingEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    //format swagger docs
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });

            //localization
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Add application services
            services.AddTransient<IVehiculoBusiness, VehiculoBusiness>();
            services.AddTransient<IVehiculoRepository>(s => new VehiculoRepository("Server=(local);Database=PAC;Trusted_Connection=True;MultipleActiveResultSets=true;"));

            services.AddEmailService(_hostingEnvironment, _configuration);//load email settings from appsettings

            //routing
            services.AddRouting();
            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "TAX INDIVIDUAL - Vehiculos RESTful API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //Configure docs visible by http://localhost:59770/docs/
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.RoutePrefix = "docs";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vehiculos API v1");
            });

            //localization
            var supportedCultures = new[] {
                new CultureInfo("en-US"),
                new CultureInfo("en"),
                new CultureInfo("es"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("es")),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider
                    {
                        QueryStringKey = "culture",
                        UIQueryStringKey = "ui-culture"
                    }
                }
            });

            app.UseMvc();

            //Configure logger
            env.ConfigureNLog("nlog.config");
            // make sure other languages chars wont mess up
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //add NLog to asp.net core
            loggerFactory.AddNLog();

            //add Nlog.Web
            app.AddNLogWeb();
        }
    }
}
