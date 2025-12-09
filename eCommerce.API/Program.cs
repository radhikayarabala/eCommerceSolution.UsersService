using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Core.Mappers;
using eCommerce.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
var builder = WebApplication.CreateBuilder(args);


// Add services
builder.Services.AddInfrastructure();
builder.Services.AddCore();

// Add controllers to the service collection
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});
builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);

builder.Services.AddFluentValidationAutoValidation();

//Add API explorer services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Cors services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
// Build the web application
var app = builder.Build();

app.UseExceptionHandlingMiddleware();

//Routing
app.UseRouting();

app.UseSwagger(); //Adds endpoint that can serve the swagger.json
app.UseSwaggerUI(); // Adds swagger UI (Interactive page to exlore and test API endpoints)
app.UseCors();
//Auth
app.UseAuthentication();
app.UseAuthorization();

//controller routes
app.MapControllers();
app.Run();
