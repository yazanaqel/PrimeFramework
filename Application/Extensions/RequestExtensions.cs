using Application.Abstractions.Messaging;
using MediatR;

namespace Application.Extensions;
public static class RequestExtensions
{

    public static bool IsQuery<T>(this IRequest<T> request)
    {
        return request is IQuery<T> && !(request is ICommand<T>);
    }

    public static bool IsCommand<T>(this IRequest<T> request)
    {
        return !request.IsQuery();
    }
}