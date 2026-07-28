using Application;
using Infrastructure;
using Prime.Identity.Queries.Application.Abstractions.Auth;
using Prime.Identity.Queries.Application.Abstractions.Filters;
using Prime.Identity.Queries.WebApi.Configuration.Jwt;
using Prime.Identity.Queries.WebApi.Middlewares;
using Prime.Identity.Queries.WebApi.Middlewares.Exceptions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddLocalIdentity();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentUserService,CurrentUserService>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<PerformanceFilter>();
    options.Filters.Add<LoggingFilter>();
});


// CORS for Blazor WASM or other front-end clients
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalBlazor",policy =>
    {
        policy.WithOrigins("https://localhost:7075")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
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
else
{
    app.UseHsts();
}

app.UseGlobalExceptionHandling();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSerilogRequestLogging(options =>
{
    options.IncludeQueryInRequestPath = true;
});


app.UseRouting();

app.UseCors("AllowLocalBlazor");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
