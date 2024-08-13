using AutoMapper;
using CityTrivia.Infrastructure.Services;
using CityTrivia.DataAccess;
using Serilog;
using CityTrivia.Infrastructure.Middleware;

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
builder.Services.AddPersistenceService(builder.Configuration["ConnectionString:CitiesTriviaDb"]);


var app = builder.Build();
app.MapControllers();
app.UseSwaggerInterface();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<PerformanceTrackerMiddleware>();

app.Run();
