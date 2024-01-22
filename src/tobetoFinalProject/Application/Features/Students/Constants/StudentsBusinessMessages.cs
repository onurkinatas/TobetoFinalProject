namespace Application.Features.Students.Constants;

public static class StudentsBusinessMessages
{
    public const string StudentNotExists = "Student not exists.";

    public static string? StudentShouldBeExist { get; internal set; }
}