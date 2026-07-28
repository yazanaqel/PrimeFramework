using Application;
using Infrastructure;
using Infrastructure.DatabaseSeed;
using Microsoft.EntityFrameworkCore;
using Prime.Identity.Application.Abstractions.Auth;
using Prime.Identity.Infrastructure.Services;
using Prime.Identity.WebApi;
using Prime.Identity.WebApi.Endpoints.Auth;
using Prime.Identity.WebApi.Endpoints.Business;
using Serilog;
using WebApi.Exceptions;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddLocalIdentity();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentUserService,CurrentUserService>();

// CORS for Blazor WASM or other front-end clients
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalBlazor", policy =>
    {
        policy.WithOrigins("https://localhost:7075")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Health checks
builder.Services.AddHealthChecks();


builder.Host.UseSerilog((context,config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});


var app = builder.Build();

using(var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;

    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    db.Database.Migrate();

    var seeder = services.GetRequiredService<ISeeder>();

    await seeder.Initialize();

}

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

//app.MapIdentityApi<User>();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSerilogRequestLogging(options =>
{
    options.IncludeQueryInRequestPath = true;
});

//app.UseHangfireDashboard("/hangfire");

app.UseRouting();

app.UseCors("AllowLocalBlazor");

app.UseAuthentication();

app.UseAuthorization();

app.MapHealthChecks("/health");

app.MapControllers();

app.MapAuthEndpoints();
app.MapCategoryEndpoints();

app.Run();
