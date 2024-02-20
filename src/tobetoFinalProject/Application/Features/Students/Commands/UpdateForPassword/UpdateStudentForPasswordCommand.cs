using Application.Features.Students.Constants;
using Application.Features.Users.Commands.UpdateFromAuth;
using Application.Features.Users.Rules;
using Application.Services.AuthService;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Entities;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Students.Commands.UpdateForPassword;
public class UpdateStudentForPasswordCommand : IRequest<UpdatedUserFromAuthResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int? UserId { get; set; }
    public string LastPassword { get; set; }
    public string NewPassword { get; set; }
    public string CheckNewPassword { get; set; }

    public string[] Roles => new[] { "Student" };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => $"GetStudent{UserId}";


    public class UpdateStudentForPasswordCommandHandler : IRequestHandler<UpdateStudentForPasswordCommand, UpdatedUserFromAuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IContextOperationService _contextOperationService;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IAuthService _authService;

        public UpdateStudentForPasswordCommandHandler(
            IUserRepository userRepository,
            IMapper mapper,
            UserBusinessRules userBusinessRules,
            IAuthService authService
,
            IContextOperationService contextOperationService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
            _authService = authService;
            _contextOperationService = contextOperationService;
        }

        public async Task<UpdatedUserFromAuthResponse> Handle(UpdateStudentForPasswordCommand request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            User? user = await _userRepository.GetAsync(predicate: u => u.Id == getStudent.UserId, cancellationToken: cancellationToken);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _userBusinessRules.UserPasswordShouldBeMatched(user: user!, request.LastPassword);
            await _userBusinessRules.UserPasswordAndCheckPassword(request.NewPassword, request.CheckNewPassword);
            user = _mapper.Map(request, user);

            HashingHelper.CreatePasswordHash(
                request.NewPassword,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            user!.PasswordHash = passwordHash;
            user!.PasswordSalt = passwordSalt;

            User updatedUser = await _userRepository.UpdateAsync(user!);

            UpdatedUserFromAuthResponse response = _mapper.Map<UpdatedUserFromAuthResponse>(updatedUser);
            response.AccessToken = await _authService.CreateAccessToken(user!);
            return response;
        }
    }
}
