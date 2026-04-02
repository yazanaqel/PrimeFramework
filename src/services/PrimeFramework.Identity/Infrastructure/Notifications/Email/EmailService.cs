using Application.Notifications.Email;
using FluentEmail.Core;

namespace Infrastructure.Notifications.Email;

internal class EmailService(IFluentEmail email):IEmailService
{

    private readonly IFluentEmail _email = email;
    public async Task SendEmailAsync(string to,string subject,string body)
    {
        await _email.To(to).Subject(subject).Body(body,isHtml: true).SendAsync();
    }

}
