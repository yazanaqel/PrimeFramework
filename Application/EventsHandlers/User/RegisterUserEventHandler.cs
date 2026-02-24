using Domain.Entities.Users;
using MediatR;

namespace Application.EventsHandlers.User;

public sealed class RegisterUserEventHandler : INotificationHandler<UserRegisteredEvent>
{
    public Task Handle(UserRegisteredEvent notification,CancellationToken ct)
    {



        Console.WriteLine("Register_User_Event_Handler");

        return Task.CompletedTask;
    }
}
