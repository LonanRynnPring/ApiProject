using ApprenticeWebAPI.ApplicationLayer;
using ApprenticeWebAPI.ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ApprenticeWebAPI
{
    /// <summary>
    /// The startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Start up constructor
        /// </summary>
        /// <param name="configuration">configuration interface</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration interface
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection Interface</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                var jsonOutputFormatter = setupAction.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();

                if (jsonOutputFormatter != null && jsonOutputFormatter.SupportedMediaTypes.Contains("text/json"))
                {
                    jsonOutputFormatter.SupportedMediaTypes.Remove("text/json");
                }
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc($"ApprenticeWebAPI", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = $"Apprentice Web API",
                    Version = "1"
                });

                setupAction.DocInclusionPredicate((documentName, apiDescription) =>
                {
                    return true;
                });
                setupAction.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });

            services.AddScoped<IAccountsLogic, AccountsLogic>();
            services.AddScoped<IExampleLogic, ExampleLogic>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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

            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint($"/swagger/" + $"ApprenticeWebAPI/swagger.json", "Apprentice Web API");
                setupAction.EnableDeepLinking();
                setupAction.DisplayOperationId();
                setupAction.DisplayRequestDuration();
                setupAction.EnableValidator();
                setupAction.RoutePrefix = "";
            });
            app.UseMvc();
        }
    }
}

