using BooksDemo.Data.DataAccess;
using BooksDemo.Logging;
using BooksDemo.Logging.Interface;
using BooksDemo.Models.Mapping;
using BooksDemo.Repository;
using BooksDemo.Repository.Interface;
using BooksDemo.Service;
using BooksDemo.Service.Interface;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IBooksService, BooksService>();
builder.Services.AddTransient<IBooksRepository, BooksRepository>();
builder.Services.AddAutoMapper(typeof(BookMapper));
builder.Services.AddSingleton<ILog, Log>();
builder.Services.AddHealthChecks().AddDbContextCheck<BooksDBContext>().AddApplicationInsightsPublisher(); 
builder.Services.AddHealthChecksUI().AddInMemoryStorage();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
        Version = "v1",
        Title = "Swagger API",
        Description = "Simple Web API with CURD operations"
    });
    c.ResolveConflictingActions(x => x.First());
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});

var connectionString = builder.Configuration.GetConnectionString("BooksDB");
builder.Services.AddDbContext<BooksDBContext>(options => options.UseSqlServer(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("./v1/swagger.json", "My API V1");
    });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHealthChecks("/Health");

app.UseHealthChecks("/healthcheck", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse //nuget: AspNetCore.HealthChecks.UI.Client
});

//nuget: AspNetCore.HealthChecks.UI
app.UseHealthChecksUI(options =>
{
    options.UIPath = "/healthchecks-ui";
    options.ApiPath = "/health-ui-api";
});

app.UseStaticFiles();

app.Run();
