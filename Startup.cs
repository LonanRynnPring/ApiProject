using ApprenticeWebAPI.ApplicationLayer;
using ApprenticeWebAPI.ApplicationLayer.Interfaces;
using ApprenticeWebAPI.DataAccessLayer;
using ApprenticeWebAPI.DataAccessLayer.Interfaces;
using ApprenticeWebAPI.Utility;
using ApprenticeWebAPI.Utility.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Serialization;

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
            services.AddControllers(setupAction => setupAction.ReturnHttpNotAcceptable = true)
                .AddXmlDataContractSerializerFormatters()
                .AddNewtonsoftJson(setupAction => setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

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

            #region Application Layer
            services.AddScoped<IAccountsLogic, AccountsLogic>();
            services.AddScoped<IExampleLogic, ExampleLogic>();
            services.AddScoped<IAddressesLogic, AddressesLogic>();
            services.AddScoped<ICardsLogic, CardsLogic>();
            #endregion Application Layer

            #region Data Access Layer
            services.AddScoped<IDataHelper, DataHelper>();
            services.AddScoped<IExamplesRepository, ExamplesRepository>();
            #endregion Data Access Layer
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

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
        }
    }
}
