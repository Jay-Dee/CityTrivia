using Asp.Versioning.ApiExplorer;
using AutoMapper;
using CityTrivia.Infrastructure;
using CityTrivia.WebApi.DbContext;
using CityTrivia.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Identity.Web;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthenticationService(builder.Configuration);
builder.Services.AddAuthorizationService();
builder.Services.AddApiVersioningService();
builder.Services.AddSwaggerService();
builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
builder.Services.AddDbContext<ICitiesTriviaDbContext, CitiesTriviaDbContext>(options => options.UseSqlite(builder.Configuration["ConnectionString:CitiesTriviaDb"]));
builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();


var app = builder.Build();
app.MapControllers();
app.UseSwaggerInterface();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
