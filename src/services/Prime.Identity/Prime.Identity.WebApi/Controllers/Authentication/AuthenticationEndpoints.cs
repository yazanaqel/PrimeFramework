using Application.Features.Authentication.LoginUser;
using Application.Features.Authentication.RegisterUser;
using Application.Features.User.LogoutUser;
using Application.Features.User.RefreshToken;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Prime.Identity.Domain.Entities.Users;

namespace WebApi.Controllers.Authentication;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this WebApplication app)
    {
        app.MapPost("/Users/Register",async (RegisterUserRequest request,IMediator mediator,CancellationToken ct) =>
        {
            var response = await mediator.Send(new RegisterUserCommand(request,ct));

            return response.IsSuccess ? Results.Ok(response.Value) : Results.BadRequest(response.Error);
        });

        app.MapPost("/Users/Login",async (LoginUserRequest request,IMediator mediator,CancellationToken ct) =>
        {
            var response = await mediator.Send(new LoginUserCommand(request,ct));

            return response.IsSuccess ? Results.Ok(response.Value) : Results.BadRequest(response.Error);
        });

        app.MapPost("/Users/Refresh",async (RefreshTokenRequest request,IMediator mediator,CancellationToken ct) =>
        {
            var response = await mediator.Send(new RefreshTokenCommand(request,ct));

            return response.IsSuccess ? Results.Ok(response.Value) : Results.BadRequest(response.Error);
        });

        app.MapPost("/Users/Logout/{userId}",async (string? userId,IMediator mediator,CancellationToken ct) =>
        {
            if(UserId.TryParse(userId,out UserId parsedUserId))
            {
                var response = await mediator.Send(new LogoutUserCommand(parsedUserId,ct));

                return response.IsSuccess ? Results.Ok(response.Value) : Results.BadRequest(response.Error);
            }

            return Results.BadRequest(new { valid = false,message = "Invalid GUID format." });

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
                        .Select(e => new { e.PropertyName,e.ErrorMessage });

                    await context.Response.WriteAsJsonAsync(errors);
                    return;
                }

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            });
        });

    }
}
