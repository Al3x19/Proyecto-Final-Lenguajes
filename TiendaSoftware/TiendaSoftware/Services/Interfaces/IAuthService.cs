using System.Security.Claims;
using TiendaSoftware.Dtos.Auth;
using TiendaSoftware.DTOS.Auth;
using TiendaSoftware.DTOS.Common;

namespace TiendaSoftware.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
        Task<ResponseDto<LoginResponseDto>> RegisterAsync(RegisterDto dto);

        Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync(RefreshTokenDto dto);
        ClaimsPrincipal GetTokenPrincipal(string token);
        Task<ResponseDto<LoginResponseDto>> RegisterDevAsync(RegisterDto dto);
    }
}

