using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.CacheForMemory;
using Application.Services.Students;
using Application.Services.UsersService;
using Core.Application.Dtos;
using Core.Application.Pipelines.Caching;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoggedResponse>, ICacheRemoverRequest
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string IpAddress { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }

    public string CacheGroupKey => "Get";

    public LoginCommand()
    {
        UserForLoginDto = null!;
        IpAddress = string.Empty;
    }

    public LoginCommand(UserForLoginDto userForLoginDto, string ipAddress)
    {
        UserForLoginDto = userForLoginDto;
        IpAddress = ipAddress;
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMemoryCache _memoryCache;
        private readonly IStudentsService _studentsService;
        private readonly ICacheMemoryService _cacheForMemoryService;

        public LoginCommandHandler(
                IUserService userService,
                IAuthService authService,
                AuthBusinessRules authBusinessRules,
                IAuthenticatorService authenticatorService,
                IMemoryCache memoryCache,
                IStudentsService studentsService,
                ICacheMemoryService cacheForMemoryService)
        {
            _userService = userService;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
            _memoryCache = memoryCache;
            _studentsService = studentsService;
            _cacheForMemoryService = cacheForMemoryService;
        }

        public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(
                predicate: u => u.Email == request.UserForLoginDto.Email,
                cancellationToken: cancellationToken
            );
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _authBusinessRules.UserPasswordShouldBeMatch(user!.Id, request.UserForLoginDto.Password);



            LoggedResponse loggedResponse = new();

            if (user.AuthenticatorType is not AuthenticatorType.None)
            {
                if (request.UserForLoginDto.AuthenticatorCode is null)
                {
                    await _authenticatorService.SendAuthenticatorCode(user);
                    loggedResponse.RequiredAuthenticatorType = user.AuthenticatorType;
                    return loggedResponse;
                }

                await _authenticatorService.VerifyAuthenticatorCode(user, request.UserForLoginDto.AuthenticatorCode);
            }

            AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

            Core.Security.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
            Core.Security.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
            await _authService.DeleteOldRefreshTokens(user.Id);

            loggedResponse.AccessToken = createdAccessToken;
            loggedResponse.RefreshToken = addedRefreshToken;

            Student? student = await _studentsService.GetAsync(
                predicate: s => s.UserId == user.Id,
                cancellationToken: cancellationToken);
            if (student != null)
                _cacheForMemoryService.AddStudentIdToCache(student.Id);
            loggedResponse.StudentId = student.Id;
            loggedResponse.UserFirstName = user.FirstName;
            loggedResponse.UserLastName = user.LastName;
            return loggedResponse;
        }
    }
}
