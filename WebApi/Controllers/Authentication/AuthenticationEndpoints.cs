using Application.Features.Authentication.LoginUser;
using Application.Features.Authentication.RegisterUser;
using MediatR;
using FluentValidation;
using FluentValidation.Results;

namespace WebApi.Controllers.Authentication;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this WebApplication app)
    {
        app.MapPost("/Users/Login", async (LoginUserRequest request, IMediator mediator, IValidator<LoginUserRequest> validator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var response = await mediator.Send(new LoginUserCommand(request));

            return response.IsSuccess ? Results.Ok(response.Value) : Results.NotFound(response.Error);

        });

        app.MapPost("/Users/Register", async (RegisterUserRequest request, IMediator mediator, IValidator<RegisterUserRequest> validator) =>
        {
            ValidationResult validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var response = await mediator.Send(new RegisterUserCommand(request));

            return response.IsSuccess ? Results.Ok(response.Value) : Results.NotFound(response.Error);
        });

    }
}
