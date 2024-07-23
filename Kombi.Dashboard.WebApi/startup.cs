using Kombi.Dashboard.Repository;
using Kombi.Dashboard.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Kombi.Dashboard.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração dos serviços da aplicação
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<ICidsRepository, CidsRepository>();
            services.AddScoped<ICidsService, CidsService>();
            services.AddScoped<IStibaRepository, StibaRepository>();
            services.AddScoped<IStibaService, StibaService>();
            services.AddScoped<ISaudeRepository, SaudeRepository>();
            services.AddScoped<ISaudeService, SaudeService>(); 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });
            });


            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:65166/")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors(builder =>
            builder.WithOrigins("http://localhost:65166/") // Permite apenas solicitações do front-end Angular
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseCors("AllowSpecificOrigin");
        }

       

    }


}
