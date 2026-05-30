using MediatR;
using SaaSBillingSystem.Application.Features.Users.UserDtos;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Users.GetUserById
{
    public class GetUserByIdHandler: IRequestHandler<GetUserByIdQuery, Result<UserDto?>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cacheService;
        public GetUserByIdHandler(IUserRepository userRepository, ICacheService cacheService)
        {
            _userRepository = userRepository;
            _cacheService = cacheService;
        }
        public async Task<Result<UserDto?>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var cachekey = $"user:{query.Id}";

            var cachedUser = await _cacheService.GetAsync<UserDto>(cachekey);

            if(cachedUser != null)
            {
                return Result<UserDto?>.Success(cachedUser);
            }

            var user = await _userRepository.GetByIdAsync(query.Id);

            if (user == null)
            {
                return Result<UserDto?>.Failure($"No user found with id {query.Id}");
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
            };

            await _cacheService.SetAsync(cachekey, userDto, TimeSpan.FromMinutes(3));

            return Result<UserDto?>.Success(userDto);
        }
    }
}
