using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common.ConfigurationOptions;

namespace SaaSBillingSystem.Infrastructure.Services
{
    public class EmailService: IEmailService
    {
        private readonly EmailOptions _options;
        public EmailService(IOptions<EmailOptions> options)
        {
            _options = options.Value;
        }
        public async Task SendAsync(string to, string subject, string htmlBody, CancellationToken cancellationToken)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_options.FromName, _options.FromAddress));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            email.Body = new BodyBuilder
            {
                HtmlBody = htmlBody,
            }.ToMessageBody();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                    _options.Host,
                    _options.Port,
                    _options.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls,
                    cancellationToken
                );

            await smtp.AuthenticateAsync(
                    _options.Username,
                    _options.Password,
                    cancellationToken
                );

            await smtp.SendAsync(
                    email,
                    cancellationToken
                );

            await smtp.DisconnectAsync(
                    true,
                    cancellationToken
                );
        }
    }
}
