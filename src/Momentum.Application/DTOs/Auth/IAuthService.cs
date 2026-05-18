using Momentum.Application.DTOs.Auth;

namespace Momentum.Application.DTOs.Auth;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}