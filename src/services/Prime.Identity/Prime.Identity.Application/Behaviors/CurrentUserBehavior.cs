using MediatR;
using Prime.Identity.Application.Abstractions.Auth;

namespace Prime.Identity.Application.Behaviors;

//public class CurrentUserBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest,TResponse> where TRequest : notnull
//{
//    private readonly ICurrentUserService _currentUser;
//    public CurrentUserBehavior(ICurrentUserService currentUser) => _currentUser = currentUser;
//    public async Task<TResponse> Handle(TRequest request,RequestHandlerDelegate<TResponse> next,CancellationToken ct)
//    {
//        if(request is IRequireUser req && _currentUser.IsAuthenticated)
//            req.UserId = _currentUser.UserId;

//        return await next();
//    }

//}
