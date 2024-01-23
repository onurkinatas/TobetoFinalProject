using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.UsersService;
using Core.Application.Dtos;
using Core.Application.Pipelines.Caching;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.StudentLogin;
public class StudentLoginCommand : IRequest<StudentLoggedResponse>, ICacheRemoverRequest
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string IpAddress { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }

    public string CacheGroupKey => "Get";

    public StudentLoginCommand()
    {
        UserForLoginDto = null!;
        IpAddress = string.Empty;
    }

    public StudentLoginCommand(UserForLoginDto userForLoginDto, string ipAddress)
    {
        UserForLoginDto = userForLoginDto;
        IpAddress = ipAddress;
    }

    public class StudentLoginCommandHandler : IRequestHandler<StudentLoginCommand, StudentLoggedResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public StudentLoginCommandHandler(
                IUserService userService,
                IAuthService authService,
                AuthBusinessRules authBusinessRules)
               
        {
            _userService = userService;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<StudentLoggedResponse> Handle(StudentLoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(
                predicate: u => u.Email == request.UserForLoginDto.Email,
                cancellationToken: cancellationToken
            );
            await _authBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _authBusinessRules.UserPasswordShouldBeMatch(user!.Id, request.UserForLoginDto.Password);
            await _authBusinessRules.UserShouldBeStudent(user.Id);


            StudentLoggedResponse loggedResponse = new();

            AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

            Core.Security.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
            Core.Security.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
            await _authService.DeleteOldRefreshTokens(user.Id);

            loggedResponse.AccessToken = createdAccessToken;
            loggedResponse.RefreshToken = addedRefreshToken;

            return loggedResponse;
        }
    }
}



