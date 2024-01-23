using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;

namespace Application.Features.Auth.Rules;

public class AuthBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly IOperationClaimRepository _operationClaimRepository;


    public AuthBusinessRules(IUserRepository userRepository, IEmailAuthenticatorRepository emailAuthenticatorRepository, IStudentRepository studentRepository, IUserOperationClaimRepository userOperationClaimRepository, IOperationClaimRepository operationClaimRepository)
    {
        _userRepository = userRepository;
        _emailAuthenticatorRepository = emailAuthenticatorRepository;
        _studentRepository = studentRepository;
        _userOperationClaimRepository= userOperationClaimRepository ;
        _operationClaimRepository = operationClaimRepository;
    }

    public Task EmailAuthenticatorShouldBeExists(EmailAuthenticator? emailAuthenticator)
    {
        if (emailAuthenticator is null)
            throw new BusinessException(AuthMessages.EmailAuthenticatorDontExists);
        return Task.CompletedTask;
    }

    public Task OtpAuthenticatorShouldBeExists(OtpAuthenticator? otpAuthenticator)
    {
        if (otpAuthenticator is null)
            throw new BusinessException(AuthMessages.OtpAuthenticatorDontExists);
        return Task.CompletedTask;
    }

    public Task OtpAuthenticatorThatVerifiedShouldNotBeExists(OtpAuthenticator? otpAuthenticator)
    {
        if (otpAuthenticator is not null && otpAuthenticator.IsVerified)
            throw new BusinessException(AuthMessages.AlreadyVerifiedOtpAuthenticatorIsExists);
        return Task.CompletedTask;
    }

    public Task EmailAuthenticatorActivationKeyShouldBeExists(EmailAuthenticator emailAuthenticator)
    {
        if (emailAuthenticator.ActivationKey is null)
            throw new BusinessException(AuthMessages.EmailActivationKeyDontExists);
        return Task.CompletedTask;
    }

    public Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            throw new BusinessException(AuthMessages.UserDontExists);
        return Task.CompletedTask;
    }

    public Task UserShouldNotBeHaveAuthenticator(User user)
    {
        if (user.AuthenticatorType != AuthenticatorType.None)
            throw new BusinessException(AuthMessages.UserHaveAlreadyAAuthenticator);
        return Task.CompletedTask;
    }

    public Task RefreshTokenShouldBeExists(RefreshToken? refreshToken)
    {
        if (refreshToken == null)
            throw new BusinessException(AuthMessages.RefreshDontExists);
        return Task.CompletedTask;
    }

    public Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
    {
        if (refreshToken.Revoked != null && DateTime.UtcNow >= refreshToken.Expires)
            throw new BusinessException(AuthMessages.InvalidRefreshToken);
        return Task.CompletedTask;
    }

    public async Task UserEmailShouldBeNotExists(string email)
    {
        bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Email == email, enableTracking: false);
        if (doesExists)
            throw new BusinessException(AuthMessages.UserMailAlreadyExists);
    }
    public async Task UserShouldBeStudent(int userId)
    {
        bool doesExists = await _studentRepository.AnyAsync(predicate: s =>s.UserId==userId, enableTracking: false);
        if (!doesExists)
            throw new BusinessException(AuthMessages.UserIsNotAStudent);
    }
    public async Task UserShouldBeAdmin(int userId)
    {
        await UserOperationClaimShouldBeExist(userId);
        UserOperationClaim userOperationClaim = await _userOperationClaimRepository.GetAsync(predicate: s => s.UserId == userId, enableTracking: false);
        await OperationClaimShouldBeExist(userOperationClaim.OperationClaimId);
        OperationClaim operationClaim  = await _operationClaimRepository.GetAsync(predicate:oc=>oc.Id==userOperationClaim.OperationClaimId, enableTracking: false);
        if (operationClaim.Name!="Admin")
            throw new BusinessException(AuthMessages.UserIsNotAAdmin);
    }
    public async Task OperationClaimShouldBeExist(int operationClaimId)
    {
        bool doesExist = await _operationClaimRepository.AnyAsync(predicate: oc => oc.Id == operationClaimId, enableTracking: false);
        if (!doesExist)
            throw new BusinessException(AuthMessages.OperationClaimShouldBeExist);
    }
    public async Task UserOperationClaimShouldBeExist(int userId)
    {
        bool doesExist = await _userOperationClaimRepository.AnyAsync(predicate: s => s.UserId == userId, enableTracking: false);
        if (!doesExist)
            throw new BusinessException(AuthMessages.UserOperationClaimShouldBeExist);
    }
    public async Task UserPasswordShouldBeMatch(int id, string password)
    {
        User? user = await _userRepository.GetAsync(predicate: u => u.Id == id, enableTracking: false);
        await UserShouldBeExistsWhenSelected(user);
        if (!HashingHelper.VerifyPasswordHash(password, user!.PasswordHash, user.PasswordSalt))
            throw new BusinessException(AuthMessages.PasswordDontMatch);
    }
}
