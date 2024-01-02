namespace WeLearnOnine_Website.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(List<string> toAddresses, string subject, string body);

        Task SendPaymentReminderEmailAsync(string toEmail, string userName, string billCode);
    }
}
