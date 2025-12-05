using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Core.Mappers;
using eCommerce.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

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

// Build the web application
var app = builder.Build();

app.UseExceptionHandlingMiddleware();

//Routing
app.UseRouting();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//controller routes
app.MapControllers();
app.Run();
