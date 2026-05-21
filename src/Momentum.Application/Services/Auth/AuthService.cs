using Momentum.Application.DTOs.Auth;
using Momentum.Application.Interfaces.Auth;
using Momentum.Application.Interfaces.Persistence;
using Momentum.Domain.Entities;
using Momentum.Application.Common.Security;

namespace Momentum.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        IUserRepository userRepository,
        JwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userRepository
            .GetByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            throw new Exception("User already exists.");
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = PasswordHasher.Hash(request.Password)
        };

        await _userRepository.AddAsync(user);

        await _userRepository.SaveChangesAsync();

        var token = _jwtTokenGenerator.Generate(user);

        return new AuthResponse
        {
            Token = token
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository
            .GetByEmailAsync(request.Email);

        if (user is null)
        {
            throw new Exception("Invalid credentials.");
        }

        var validPassword = PasswordHasher.Verify(
            request.Password,
            user.PasswordHash);

        if (!validPassword)
        {
            throw new Exception("Invalid credentials.");
        }

        var token = _jwtTokenGenerator.Generate(user);

        return new AuthResponse
        {
            Token = token
        };
    }
}