using Application.Services.ContextOperations;
using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("nArchitecture"));

        services.AddDbContext<BaseDbContext>(
                        options => options
                        .UseSqlServer(configuration
                        .GetConnectionString
                        ("TobetoPlatformConnectionString")
                    ));

        services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
        services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();

        services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
       
        services.AddScoped<IAppealRepository, AppealRepository>();
        services.AddScoped<IAppealStageRepository, AppealStageRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICertificateRepository, CertificateRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IClassAnnouncementRepository, ClassAnnouncementRepository>();
        services.AddScoped<IClassExamRepository, ClassExamRepository>();
        services.AddScoped<IClassLectureRepository, ClassLectureRepository>();
        services.AddScoped<IClassSurveyRepository, ClassSurveyRepository>();
        services.AddScoped<IContentRepository, ContentRepository>();
        services.AddScoped<IContentCategoryRepository, ContentCategoryRepository>();
        services.AddScoped<IContentInstructorRepository, ContentInstructorRepository>();
        services.AddScoped<IContentLikeRepository, ContentLikeRepository>();
        services.AddScoped<IContentTagRepository, ContentTagRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseContentRepository, CourseContentRepository>();
        services.AddScoped<IDistrictRepository, DistrictRepository>();
        services.AddScoped<IExamRepository, ExamRepository>();
        services.AddScoped<IInstructorRepository, InstructorRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<ILanguageLevelRepository, LanguageLevelRepository>();
        services.AddScoped<ILectureRepository, LectureRepository>();
        services.AddScoped<ILectureCompletionConditionRepository, LectureCompletionConditionRepository>();
        services.AddScoped<ILectureCourseRepository, LectureCourseRepository>();
        services.AddScoped<ILectureLikeRepository, LectureLikeRepository>();
        services.AddScoped<ILectureSpentTimeRepository, LectureSpentTimeRepository>();
        services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
        services.AddScoped<IStageRepository, StageRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentAnnouncementRepository, StudentAnnouncementRepository>();
        services.AddScoped<IStudentAppealRepository, StudentAppealRepository>();
        services.AddScoped<IStudentCertificateRepository, StudentCertificateRepository>();
        services.AddScoped<IStudentClassRepository, StudentClassRepository>();
        services.AddScoped<IStudentClassStudentRepository, StudentClassStudentRepository>();
        services.AddScoped<IStudentEducationRepository, StudentEducationRepository>();
        services.AddScoped<IStudentExamRepository, StudentExamRepository>();
        services.AddScoped<IStudentExperienceRepository, StudentExperienceRepository>();
        services.AddScoped<IStudentLanguageLevelRepository, StudentLanguageLevelRepository>();
        services.AddScoped<IStudentSkillRepository, StudentSkillRepository>();
        services.AddScoped<IStudentSocialMediaRepository, StudentSocialMediaRepository>();
        services.AddScoped<IStudentStageRepository, StudentStageRepository>();
        services.AddScoped<ISubTypeRepository, SubTypeRepository>();
        services.AddScoped<ISurveyRepository, SurveyRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IStudentSurveyRepository, StudentSurveyRepository>();
       services.AddScoped<ILectureViewRepository, LectureViewRepository>();
       services.AddScoped<IStudentPrivateCertificateRepository, StudentPrivateCertificateRepository>();
       services.AddScoped<IOptionRepository, OptionRepository>();
       services.AddScoped<IQuestionRepository, QuestionRepository>();
       services.AddScoped<IQuestionOptionRepository, QuestionOptionRepository>();
       services.AddScoped<IQuizRepository, QuizRepository>();
       services.AddScoped<IQuizQuestionRepository, QuizQuestionRepository>();
       services.AddScoped<IPoolRepository, PoolRepository>();
       services.AddScoped<IPoolQuestionRepository, PoolQuestionRepository>();
       services.AddScoped<IStudentQuizOptionRepository, StudentQuizOptionRepository>();
       services.AddScoped<IGeneralQuizRepository, GeneralQuizRepository>();
       services.AddScoped<IClassQuizRepository, ClassQuizRepository>();
       services.AddScoped<IStudentQuizResultRepository, StudentQuizResultRepository>();
       services.AddScoped<ICommentSubCommentRepository, CommentSubCommentRepository>();
       services.AddScoped<IStudentLectureCommentRepository, StudentLectureCommentRepository>();
        return services;
    }
}
