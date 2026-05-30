using MediatR;
using SaaSBillingSystem.Application.Features.Users.UserDtos;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Users.GetUserById
{
    public class GetUserByIdQuery: IRequest<Result<UserDto?>>
    {
        public Guid Id { get; set; }
        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
