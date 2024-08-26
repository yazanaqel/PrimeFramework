﻿using Domain.Shared;
using MediatR;

namespace Application.Abstractions.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
public interface IQueryHandler<TQuery, TResponse>
: IRequestHandler<TQuery,Result<TResponse>>
where TQuery : IQuery<TResponse>
{
}
