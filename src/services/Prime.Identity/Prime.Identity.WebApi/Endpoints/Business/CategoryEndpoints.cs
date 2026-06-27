using MediatR;
using Prime.Identity.Application.Features.Category.Create;

namespace Prime.Identity.WebApi.Endpoints.Business;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        app.MapPost("/Category/Create",async (CreateCategoryRequest request,IMediator mediator,CancellationToken ct) =>
        {
            var response = await mediator.Send(new CreateCategoryCommand(request,ct));

            return response.IsSuccess ? Results.Ok(response.Value) : Results.BadRequest(response.Error);
        });
    }
}