using Application.Abstractions;
using Hangfire;
using System.Linq.Expressions;

namespace Infrastructure.Jobs.HangfireAdapter;

public class HangfireJobScheduler : IJobScheduler
{
    public string Enqueue(Expression<Func<Task>> methodCall)
    {
        return BackgroundJob.Enqueue(methodCall);
    }

    public string Schedule(Expression<Func<Task>> methodCall,TimeSpan delay)
    {
        return BackgroundJob.Schedule(methodCall,delay);
    }
}
