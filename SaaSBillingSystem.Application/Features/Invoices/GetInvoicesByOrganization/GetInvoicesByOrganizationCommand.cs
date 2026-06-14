using MediatR;
using SaaSBillingSystem.Application.Features.Invoices.InvoicesDTOs;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invoices.GetInvoicesByOrganization
{
    public class GetInvoicesByOrganizationCommand: IRequest<Result<List<InvoiceResponse>>>
    {
        public Guid OrganizationId { get; private set; }

        public GetInvoicesByOrganizationCommand(Guid organizationId)
        {
            OrganizationId = organizationId;
        }
    }
}
