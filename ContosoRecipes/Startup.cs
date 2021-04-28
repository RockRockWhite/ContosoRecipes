using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoRecipes.Models;
using ContosoRecipes.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace ContosoRecipes
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
            services.Configure<CookbookDatabaseSettings>(Configuration.GetSection(nameof(CookbookDatabaseSettings)));
            // TODO 弄清楚本句含义
            services.AddSingleton<CookbookDatabaseSettings>(provider =>
                provider.GetRequiredService<IOptions<CookbookDatabaseSettings>>().Value);
            services.AddSingleton<RecipesService>();

            services.AddControllers();
            // TODO AddSwaggerGenNewtonsoftSupport
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ContosoRecipes", Version = "v1"});
            }).AddSwaggerGenNewtonsoftSupport();

            // TODO UseMemberCasing
            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContosoRecipes v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}