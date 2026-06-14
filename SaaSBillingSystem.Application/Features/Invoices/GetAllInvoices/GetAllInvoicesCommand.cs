using MediatR;
using SaaSBillingSystem.Application.Features.Invoices.InvoicesDTOs;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invoices.GetAllInvoices
{
    public class GetAllInvoicesCommand: IRequest<Result<List<InvoiceResponse>>>
    {
    }
}
