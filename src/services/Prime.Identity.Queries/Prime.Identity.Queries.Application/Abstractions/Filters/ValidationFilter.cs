using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Prime.Identity.Queries.Application.Abstractions.Filters;

public class ValidationFilter(IServiceProvider provider) : IAsyncActionFilter
{
    private readonly IServiceProvider _provider = provider;

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        // Get the request model (DTO)
        var model = context.ActionArguments.Values.FirstOrDefault();
        if(model == null)
        {
            await next();
            return;
        }

        // Resolve validator dynamically
        var validatorType = typeof(IValidator<>).MakeGenericType(model.GetType());
        var validator = _provider.GetService(validatorType) as IValidator;

        if(validator != null)
        {
            var validationContext = new ValidationContext<object>(model);
            ValidationResult result = await validator.ValidateAsync(validationContext);

            if(!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                context.Result = new BadRequestObjectResult(new { errors });
                return;
            }
        }

        await next();
    }
}
