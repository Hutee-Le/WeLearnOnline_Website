﻿using System.Drawing.Drawing2D;
using WeLearnOnine_Website.Models;

namespace WeLearnOnine_Website.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(List<string> toAddresses, string subject, string body);

        Task SendPaymentReminderEmailAsync(string toEmail, string userName, string billCode);

        Task SendEmailVerificationAsync(string toEmail, string userName, string subject, string verificationLink);

        Task SendOrderStatusEmailAsync(Bill bill, string userName, string orderStatus, List<Course> courses);
    }
}
