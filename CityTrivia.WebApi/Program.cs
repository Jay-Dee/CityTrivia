using Asp.Versioning.ApiExplorer;
using AutoMapper;
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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(builder.Configuration);

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("AuthZPolicy", policyBuilder =>
        policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement() { RequiredScopesConfigurationKey = $"AzureAd:Scopes" }));
});

builder.Services.AddApiVersioning(setupAction => 
{ 
    setupAction.ReportApiVersions = true; 
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new Asp.Versioning.ApiVersion(1,0);
}).AddMvc()
.AddApiExplorer(setupAction => setupAction.SubstituteApiVersionInUrl = true);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
builder.Services.AddDbContext<ICitiesTriviaDbContext, CitiesTriviaDbContext>(options => options.UseSqlite(builder.Configuration["ConnectionString:CitiesTriviaDb"]));
builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var apiDescriptionProvider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

builder.Services.AddSwaggerGen(setupAction =>
{
    foreach(var service in apiDescriptionProvider.ApiVersionDescriptions)
    {
        setupAction.SwaggerDoc($"{service.GroupName}", new()
        {
            Title = "City Trivia Info",
            Version = service.ApiVersion.ToString(),
            Description = $"Documentation for {service.ApiVersion.ToString()}"
        });
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(setupAction =>
{
    var versions = app.DescribeApiVersions();
    foreach(var version in versions)
    {
        setupAction.SwaggerEndpoint($"{version.GroupName}/swagger.json", 
            version.GroupName.ToUpperInvariant());
    }

});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
