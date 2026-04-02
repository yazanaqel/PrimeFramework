using System.Linq.Expressions;

namespace Application.Abstractions;

public interface IJobScheduler
{
    string Enqueue(Expression<Func<Task>> methodCall);
    string Schedule(Expression<Func<Task>> methodCall,TimeSpan delay);
}
