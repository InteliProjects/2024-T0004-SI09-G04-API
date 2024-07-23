using Kombi.Dashboard.Repository;
using Kombi.Dashboard.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfigurationRoot configuration = configBuilder.Build();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICidsRepository>(_ => new CidsRepository(configuration["DB_CONFIG"]));
builder.Services.AddScoped<ICidsService, CidsService>();
builder.Services.AddScoped<IStibaRepository>(_ => new StibaRepository(configuration["DB_CONFIG"]));
builder.Services.AddScoped<IStibaService, StibaService>();
builder.Services.AddScoped<ISaudeRepository>(_ => new SaudeRepository(configuration["DB_CONFIG"]));
builder.Services.AddScoped<ISaudeService, SaudeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
