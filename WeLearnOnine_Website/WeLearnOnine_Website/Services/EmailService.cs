using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(List<string> toAddresses, string subject, string body)
        {
            string userName = "LeVanA";
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));

            foreach (var to in toAddresses)
            {
                message.To.Add(new MailboxAddress("LeVanB", to));
            }

            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();

            var emailBody = File.ReadAllText("EmailTemplates/WelcomeEmailTemplate.html");
            emailBody = emailBody.Replace("{UserName}", userName);
            bodyBuilder.HtmlBody = emailBody;
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        public async Task SendEmailVerificationAsync(string toEmail, string userName, string subject, string verificationLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", toEmail));

            message.Subject = subject;

            var emailBody = File.ReadAllText("EmailTemplates/RegistrationConfirmation.html");
            emailBody = emailBody.Replace("{UserName}", userName);
            emailBody = emailBody.Replace("{ConfirmationLink}", verificationLink);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = emailBody;
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ khi gửi email thất bại
                    throw ex;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

        public async Task SendPaymentReminderEmailAsync(string toEmail, string userName, string billCode)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
            message.To.Add(new MailboxAddress(userName, toEmail));

            var subject = $"👋Đơn hàng {billCode} đang chờ bạn hoàn tất.";
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();

            var emailBody = File.ReadAllText("EmailTemplates/ConfirmOrderEmailTemplate.html");
            var confirmOrderLink = "https://Welearn.bsite.net/ShoppingCart/PaymentConfirmation?billCode=" + billCode;
            emailBody = emailBody.Replace("{UserName}", userName);
            emailBody = emailBody.Replace("{BillCode}", billCode);
            emailBody = emailBody.Replace("{ConfirmOrderLink}", confirmOrderLink);

            bodyBuilder.HtmlBody = emailBody;
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ khi gửi email thất bại
                    throw ex;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
