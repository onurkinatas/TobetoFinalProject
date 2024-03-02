namespace Application.Features.Auth.Constants;

public static class AuthMessages
{
    public const string EmailAuthenticatorDontExists = "Email authenticator don't exists.";
    public const string OtpAuthenticatorDontExists = "Otp authenticator don't exists.";
    public const string AlreadyVerifiedOtpAuthenticatorIsExists = "Already verified otp authenticator is exists.";
    public const string EmailActivationKeyDontExists = "Email Activation Key don't exists.";
    public const string UserDontExists = "Kullanýcý Adý Veya Þifre Yanlýþ.";
    public const string UserHaveAlreadyAAuthenticator = "User have already a authenticator.";
    public const string RefreshDontExists = "Refresh don't exists.";
    public const string InvalidRefreshToken = "Invalid refresh token.";
    public const string UserMailAlreadyExists = "Mail zaten kullanýlýyor.";
    public const string PasswordDontMatch = "Yanlýþ E-posta veya Þifre.";
    public const string NewPasswordShouldBeDifferent = "Þifreniz son þifreyle ayný olamaz.";
    public const string UserIsNotAStudent = "Öðrenci deðilsiniz buraya giriþ yetkiniz bulunmuyor";
    public const string UserIsNotAAdmin = "Yetkili deðilsiniz buraya giriþ yetkiniz bulunmuyor";
    public const string OperationClaimShouldBeExist = "Herhangi bir yetkiniz bulunmuyor lütfen yetkililere danýþýn";
    public const string UserOperationClaimShouldBeExist = "Herhangi bir yetkiniz bulunmuyor lütfen yetkililere danýþýn";
    public const string? PasswordHaveToEqualToCheckPassword = "Yeni þifre ile ikinci þifre eþleþmiyor";
}
