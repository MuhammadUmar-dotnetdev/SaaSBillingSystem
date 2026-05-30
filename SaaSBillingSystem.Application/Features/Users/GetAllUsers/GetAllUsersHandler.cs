using MediatR;
using SaaSBillingSystem.Application.Features.Users.UserDtos;
using SaaSBillingSystem.Application.Interfaces;

namespace SaaSBillingSystem.Application.Features.Users.GetAllUsers
{
    public class GetAllUsersHandler: IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email,
            }).ToList();
        }
    }
}
