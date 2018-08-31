using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog.Web;
using PAC.Business;
using PAC.Business.Contracts;
using PAC.Common;
using PAC.Common.Extensions;
using PAC.Middlewares;
using PAC.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace PAC.VinculadoAPI
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

            // Add application services
            services.AddTransient<IVinculadoBusiness, VinculadoBusiness>();
            //services.AddTransient<IVinculadoRepository, VinculadoRepository>();
            services.AddTransient<IVinculadoRepository>(s => new VinculadoRepository(_configuration.GetConnectionString("PACConnectionString")));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "TAX INDIVIDUAL - Vinculados RESTful API", Version = "v1" });
            });

        }

        public void ConfigureCommonServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            //load config from appsettings.json
            //services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));
            services.Configure<EmailSettings>(_configuration.GetSection("Email"));

            //make these services available for the application
            // Add application services
            services.AddTransient<IVinculadoBusiness, VinculadoBusiness>();
            services.AddTransient<IVinculadoRepository>(s => new VinculadoRepository("Server=(local);Database=PAC;Trusted_Connection=True;MultipleActiveResultSets=true;"));

            services.AddEmailService(_hostingEnvironment, _configuration);
            services.AddRouting();
            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromMinutes(30);
            });
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            System.Diagnostics.Trace.WriteLine("Using Development Environment");
            ConfigureCommonServices(services);
        }

        public void ConfigureStagingServices(IServiceCollection services)
        {
            System.Diagnostics.Trace.WriteLine("Using Staging Environment");
            ConfigureCommonServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            System.Diagnostics.Trace.WriteLine("Using Production Environment");
            ConfigureCommonServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Configure log
            //loggerFactory.AddFile($"C:/@Logs/{this.GetType().FullName}.txt");

            //Configure middleware
            app.UseMiddleware(typeof(GlobalExceptionMiddleware));

            //Configure docs visible by http://localhost:59770/docs/
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                //c.RoutePrefix = "docs";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vinculado API v1"); });

            //Configure localization
            //var supportedCultures = new[] {
            //    new CultureInfo("en-US"),
            //    new CultureInfo("en"),
            //    new CultureInfo("es"),
            //};
            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture(new CultureInfo("es")),
            //    SupportedCultures = supportedCultures,
            //    SupportedUICultures = supportedCultures,
            //    RequestCultureProviders = new List<IRequestCultureProvider>
            //    {
            //        new QueryStringRequestCultureProvider
            //        {
            //            QueryStringKey = "culture",
            //            UIQueryStringKey = "ui-culture"
            //        }
            //    }
            //});

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
