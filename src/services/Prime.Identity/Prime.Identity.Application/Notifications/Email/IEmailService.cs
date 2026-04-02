namespace Application.Notifications.Email;

public interface IEmailService
{
    public Task SendEmailAsync(string to,string subject,string body);

}
