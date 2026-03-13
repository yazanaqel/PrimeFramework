using Application.Abstractions;
using Application.Notifications.Email;
using Domain.Entities.Users;
using MediatR;

namespace Application.Notifications.EventsHandlers.User;

public sealed class RegisterUserEventHandler(IJobScheduler jobScheduler, IEmailService emailService) : INotificationHandler<UserRegisteredEvent>
{
    private readonly IJobScheduler _jobScheduler = jobScheduler;
    private readonly IEmailService _emailService = emailService;

    public async Task Handle(UserRegisteredEvent notification,CancellationToken ct)
    {
        _jobScheduler.Schedule(() => _emailService.SendEmailAsync
            ("yazan.aqel93@gmail.com",
            "Email Service",
            $"<h1>New User Registered Event<h1/>"),TimeSpan.FromMinutes(2));
    }

}
