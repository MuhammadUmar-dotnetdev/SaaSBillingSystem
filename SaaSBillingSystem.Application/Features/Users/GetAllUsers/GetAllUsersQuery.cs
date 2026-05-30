using MediatR;
using SaaSBillingSystem.Application.Features.Users.UserDtos;

namespace SaaSBillingSystem.Application.Features.Users.GetAllUsers
{
    public class GetAllUsersQuery: IRequest<List<UserDto>>
    {
    }
}
