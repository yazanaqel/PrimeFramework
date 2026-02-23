using Application;
using Infrastructure;
using Infrastructure.Authentication.IdentityEntities;
using Infrastructure.DatabaseSeed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Serilog;
using WebApi.Controllers.Authentication;
using WebApi.JwtSetup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<SeederOptionsSetup>();

builder.Services.ConfigureOptions<JwtOptionsSetup>();

builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer();

builder.Services.AddAuthorization();

builder.Host.UseSerilog((context,config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging(options =>
{
    options.IncludeQueryInRequestPath = true;
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapAuthenticationEndpoints();


using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<ISeeder>();
    await seeder.Initialize();
}


app.Run();
