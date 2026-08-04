using Microsoft.Extensions.Options;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common.ConfigurationOptions;

namespace SaaSBillingSystem.Infrastructure.Services
{
    public class InvitationEmailService: IInvitationEmailService
    {
        private readonly IEmailService _emailService;
        private readonly FrontendOptions _frontend;
        public InvitationEmailService(IEmailService emailService, IOptions<FrontendOptions> options)
        {
            _emailService = emailService;
            _frontend = options.Value;
        }

        public async Task SendInvitationAsync(Invitation invitation, string organizationName, CancellationToken cancellationToken)
        {
            var subject = $"You're invited to join {organizationName}";

            var url = $"{_frontend.BaseUrl}/accept-invitation?token={invitation.Token}";

            var html = $@"
                <h2>Invitation</h2>

                <p>
                You have been invited to join
                <b>{organizationName}</b>.
                </p>

                <p>
                Role:
                <b>{invitation.Role}</b>
                </p>

                <p>
                <a href=""{url}"">
                Accept Invitation
                </a>
                </p>

                <p>
                Expires:
                {invitation.ExpiresAtUtc:u}
                </p>";

            await _emailService.SendAsync(
                invitation.Email,
                subject,
                html,
                cancellationToken);

        }
    }
}
