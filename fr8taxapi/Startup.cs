using fr8taxapi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace fr8taxapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        private string GetAccessToken(string tenantId)
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();

            string accessToken = azureServiceTokenProvider.GetAccessTokenAsync("https://ossrdbms-aad.database.windows.net", tenantId).Result;
            return accessToken;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var dbServer = Environment.GetEnvironmentVariable("DBServer").ToString();
            var database = Environment.GetEnvironmentVariable("Database").ToString();
            var dbUser = Environment.GetEnvironmentVariable("DBUser").ToString();
            var dbPassword = Environment.GetEnvironmentVariable("DBPassword").ToString();

            string connectionString = $"Server={dbServer};port=5432;user id={dbUser}@{dbServer};password={dbPassword};database={database};pooling=true";

            services.AddDbContext<TaxDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddControllers();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Tax API",
                    Description = "Tax api to get state tax details",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Yuvaraj Kesavan",
                        Email = "yuvaraj.kesavan@cognizant.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tax API V1");
                c.RoutePrefix = string.Empty;
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
