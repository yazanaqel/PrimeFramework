using Application.Features.Authentication.LoginUser;
using Application.Features.Authentication.RegisterUser;
using Application.Features.User.GetAllUsers;
using Application.Features.User.GetUserById;
using FluentValidation;
using Infrastructure.Authentication.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;

namespace WebApi.Controllers.Authentication;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this WebApplication app)
    {
        app.MapPost("/Users/Login",async (LoginUserRequest request,IMediator mediator,CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new LoginUserCommand(request,cancellationToken));

            return response.IsSuccess ? Results.Ok(response.Value) : Results.NotFound(response.Error);
        });

        app.MapPost("/Users/Register",async (RegisterUserRequest request,IMediator mediator,CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new RegisterUserCommand(request,cancellationToken));

            return response.IsSuccess ? Results.Ok(response.Value) : Results.NotFound(response.Error);
        });

        app.MapGet("/Users/GetAllUsers",async (IMediator mediator,CancellationToken cancellationToken) =>
        {
            var response = await mediator.Send(new GetAllUsersQuery(cancellationToken));

            return response.IsSuccess ? Results.Ok(response.Value) : Results.NotFound(response.Error);
        });

        app.MapGet("/Users/GetUserById/{userId}",async (string userId,IMediator mediator,CancellationToken cancellationToken) =>
        {
            if(Guid.TryParse(userId,out Guid parsedGuid))
            {
                var response = await mediator.Send(new GetUserByIdQuery(parsedGuid,cancellationToken));

                return response.IsSuccess ? Results.Ok(response.Value) : Results.NotFound(response.Error);
            }

            return Results.BadRequest(new { valid = false, message = "Invalid GUID format." });
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
