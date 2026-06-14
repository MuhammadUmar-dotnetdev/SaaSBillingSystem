using MediatR;
using SaaSBillingSystem.Application.Features.Invoices.InvoicesDTOs;
using SaaSBillingSystem.Domain.Entities;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Invoices.GetInvoiceById
{
    public class GetInvoiceByIdCommand: IRequest<Result<InvoiceResponse>>
    {
        public Guid Id { get; private set; }
        public GetInvoiceByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
