using MediatR;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.Common;

namespace SaaSBillingSystem.Application.Features.Auth.LoginUser;

public class LoginUserHandler
    : IRequestHandler<LoginUserCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;

    private readonly IJwtService _jwtService;

    private readonly IPasswordHasher _passwordHasher;
    public LoginUserHandler(
        IUserRepository userRepository,
        IJwtService jwtService,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<string>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var user =
            await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
            return Result<string>.Failure("Invalid credentials");

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            return Result<string>.Failure("Invalid credentials");

        var token = _jwtService.GenerateToken(user);

        return Result<string>.Success(token);
    }
}