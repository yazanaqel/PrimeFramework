using Application;
using Infrastructure;
using Infrastructure.DatabaseSeed;
using Serilog;
using WebApi.Controllers.Authentication;
using WebApi.Exceptions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Host.UseSerilog((context,config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();


if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging(options =>
{
    options.IncludeQueryInRequestPath = true;
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapAuthenticationEndpoints();


using(var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<ISeeder>();
    await seeder.Initialize();
}


app.Run();
