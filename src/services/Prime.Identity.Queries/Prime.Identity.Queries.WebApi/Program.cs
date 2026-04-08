using Application;
using Infrastructure;
using Prime.Identity.Queries.Application.Abstractions.Filters;
using Prime.Identity.Queries.WebApi.Middlewares;
using Prime.Identity.Queries.WebApi.Middlewares.Exceptions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<PerformanceFilter>();
    options.Filters.Add<LoggingFilter>();
});

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Host.UseSerilog((context,config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandling();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging(options =>
{
    options.IncludeQueryInRequestPath = true;
});

app.UseAuthorization();

app.MapControllers();

app.Run();
