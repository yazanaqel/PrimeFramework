using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions;

public interface ISpecification<T>
{
    Expression<Func<T,bool>>? Criteria { get; }

    IReadOnlyList<Expression<Func<T,object>>> Includes { get;}

    Expression<Func<T,object>>? OrderBy { get; }

    Expression<Func<T,object>>? OrderByDescending { get; }

    int? Skip { get; }

    int? Take { get; }

    bool IsPagingEnabled => Skip.HasValue || Take.HasValue;


}
