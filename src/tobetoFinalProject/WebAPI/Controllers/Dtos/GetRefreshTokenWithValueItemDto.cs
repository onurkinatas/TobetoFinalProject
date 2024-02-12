using Core.Security.JWT;

namespace WebAPI.Controllers.Dtos;

public class GetRefreshTokenWithValueItemDto
{
    public AccessToken AccessToken { get; set; }
    public string RefreshTokenValue { get; set; }
}
