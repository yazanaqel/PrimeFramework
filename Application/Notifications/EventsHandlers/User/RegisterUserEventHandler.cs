using Application.Notifications.Email;
using Domain.Entities.Users;
using MediatR;

namespace Application.Notifications.EventsHandlers.User;

public sealed class RegisterUserEventHandler(IEmailService emailService) : INotificationHandler<UserRegisteredEvent>
{
    private readonly IEmailService _emailService = emailService;

    public async Task Handle(UserRegisteredEvent notification,CancellationToken ct)
    {
        await _emailService.SendEmailAsync
            ("yazan.aqel93@gmail.com",
            "Email Service",
            $"<h1>Hi :)<h1/>");
    }

}
