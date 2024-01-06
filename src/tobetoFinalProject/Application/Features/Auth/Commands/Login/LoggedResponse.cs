using Core.Application.Responses;
using Core.Security.Enums;
using Core.Security.JWT;

namespace Application.Features.Auth.Commands.Login;

public class LoggedResponse : IResponse
{
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public Guid StudentId { get; set; }
    public AccessToken? AccessToken { get; set; }
    public Core.Security.Entities.RefreshToken? RefreshToken { get; set; }
    public AuthenticatorType? RequiredAuthenticatorType { get; set; }

    public LoggedHttpResponse ToHttpResponse() =>
        new() { AccessToken = AccessToken, 
            RequiredAuthenticatorType = RequiredAuthenticatorType,
            UserFirstName = UserFirstName,
            UserLastName = UserLastName,
            StudentId = StudentId
        };

    public class LoggedHttpResponse
    {
        public AccessToken? AccessToken { get; set; }
        public AuthenticatorType? RequiredAuthenticatorType { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public Guid StudentId { get; set; }
    }
}
