using Application.Features.Authentication.LoginUser;
using Application.Features.Authentication.RegisterUser;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;

namespace WebApi.Controllers.Authentication;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this WebApplication app)
    {
        app.MapPost("/Users/Login",async (LoginUserRequest request,IMediator mediator) =>
        {
            var response = await mediator.Send(new LoginUserCommand(request));

            return response.IsSuccess ? Results.Ok(response.Value) : Results.NotFound(response.Error);
        });

        app.MapPost("/Users/Register",async (RegisterUserRequest request,IMediator mediator) =>
        {
            var response = await mediator.Send(new RegisterUserCommand(request));

            return response.IsSuccess ? Results.Ok(response.Value) : Results.NotFound(response.Error);
        });


        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                if(exception is ValidationException validationException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    var errors = validationException.Errors
                        .Select(e => new { e.PropertyName,e.ErrorMessage});

                    await context.Response.WriteAsJsonAsync(errors);
                    return;
                }

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            });
        });

    }
}
