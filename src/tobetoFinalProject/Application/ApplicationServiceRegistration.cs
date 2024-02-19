using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.UsersService;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.ElasticSearch;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Services.Announcements;
using Application.Services.Appeals;
using Application.Services.AppealStages;
using Application.Services.Categories;
using Application.Services.Certificates;
using Application.Services.Cities;
using Application.Services.ClassAnnouncements;
using Application.Services.ClassExams;
using Application.Services.ClassLectures;
using Application.Services.ClassSurveys;
using Application.Services.Contents;
using Application.Services.ContentCategories;
using Application.Services.ContentInstructors;
using Application.Services.ContentLikes;
using Application.Services.ContentTags;
using Application.Services.Courses;
using Application.Services.CourseContents;
using Application.Services.Districts;
using Application.Services.Exams;
using Application.Services.Instructors;
using Application.Services.Languages;
using Application.Services.LanguageLevels;
using Application.Services.Lectures;
using Application.Services.LectureCompletionConditions;
using Application.Services.LectureCourses;
using Application.Services.LectureLikes;
using Application.Services.LectureSpentTimes;
using Application.Services.Manufacturers;
using Application.Services.Skills;
using Application.Services.SocialMedias;
using Application.Services.Stages;
using Application.Services.Students;
using Application.Services.StudentAnnouncements;
using Application.Services.StudentAppeals;
using Application.Services.StudentCertificates;
using Application.Services.StudentClasses;
using Application.Services.StudentClassStudents;
using Application.Services.StudentEducations;
using Application.Services.StudentExams;
using Application.Services.StudentExperiences;
using Application.Services.StudentLanguageLevels;
using Application.Services.StudentSkills;
using Application.Services.StudentSocialMedias;
using Application.Services.StudentStages;
using Application.Services.SubTypes;
using Application.Services.Surveys;
using Application.Services.Tags;
using Application.Services.StudentSurveys;
using Application.Services.CacheForMemory;
using Application.Services.ContextOperations;
using Application.Services.LectureViews;
using Application.Services.OperationClaims;
using Application.Services.UserOperationClaims;
using Application.Services.StudentPrivateCertificates;
using Application.Services.Options;
using Application.Services.Questions;
using Application.Services.QuestionOptions;
using Application.Services.Quizs;
using Application.Services.QuizQuestions;
using Application.Services.Pools;
using Application.Services.PoolQuestions;
using Application.Services.StudentQuizOptions;
using Application.Services.GeneralQuizs;
using Application.Services.ClassQuizs;
using Application.Services.StudentQuizResults;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMailService, MailKitMailService>();
        services.AddSingleton<LoggerServiceBase, FileLogger>();
        services.AddSingleton<IElasticSearch, ElasticSearchManager>();
        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IAuthenticatorService, AuthenticatorManager>();
        services.AddScoped<IUserService, UserManager>();
        services.AddScoped<IContextOperationService, ContextOperationManager>();
        services.AddScoped<IAnnouncementsService, AnnouncementsManager>();
        services.AddScoped<IAppealsService, AppealsManager>();
        services.AddScoped<IAppealStagesService, AppealStagesManager>();
        services.AddScoped<ICategoriesService, CategoriesManager>();
        services.AddScoped<ICertificatesService, CertificatesManager>();
        services.AddScoped<ICitiesService, CitiesManager>();
        services.AddScoped<IClassAnnouncementsService, ClassAnnouncementsManager>();
        services.AddScoped<IClassExamsService, ClassExamsManager>();
        services.AddScoped<IClassLecturesService, ClassLecturesManager>();
        services.AddScoped<IClassSurveysService, ClassSurveysManager>();
        services.AddScoped<IContentsService, ContentsManager>();
        services.AddScoped<IContentCategoriesService, ContentCategoriesManager>();
        services.AddScoped<IContentInstructorsService, ContentInstructorsManager>();
        services.AddScoped<IContentLikesService, ContentLikesManager>();
        services.AddScoped<IContentTagsService, ContentTagsManager>();
        services.AddScoped<ICoursesService, CoursesManager>();
        services.AddScoped<ICourseContentsService, CourseContentsManager>();
        services.AddScoped<IDistrictsService, DistrictsManager>();
        services.AddScoped<IExamsService, ExamsManager>();
        services.AddScoped<IInstructorsService, InstructorsManager>();
        services.AddScoped<ILanguagesService, LanguagesManager>();
        services.AddScoped<ILanguageLevelsService, LanguageLevelsManager>();
        services.AddScoped<ILecturesService, LecturesManager>();
        services.AddScoped<ILectureCompletionConditionsService, LectureCompletionConditionsManager>();
        services.AddScoped<ILectureCoursesService, LectureCoursesManager>();
        services.AddScoped<ILectureLikesService, LectureLikesManager>();
        services.AddScoped<ILectureSpentTimesService, LectureSpentTimesManager>();
        services.AddScoped<IManufacturersService, ManufacturersManager>();
        services.AddScoped<ISkillsService, SkillsManager>();
        services.AddScoped<ISocialMediasService, SocialMediasManager>();
        services.AddScoped<IStagesService, StagesManager>();
        services.AddScoped<IStudentsService, StudentsManager>();
        services.AddScoped<IStudentAnnouncementsService, StudentAnnouncementsManager>();
        services.AddScoped<IStudentAppealsService, StudentAppealsManager>();
        services.AddScoped<IStudentCertificatesService, StudentCertificatesManager>();
        services.AddScoped<IStudentClassesService, StudentClassesManager>();
        services.AddScoped<IStudentClassStudentsService, StudentClassStudentsManager>();
        services.AddScoped<IStudentEducationsService, StudentEducationsManager>();
        services.AddScoped<IStudentExamsService, StudentExamsManager>();
        services.AddScoped<IStudentExperiencesService, StudentExperiencesManager>();
        services.AddScoped<IStudentLanguageLevelsService, StudentLanguageLevelsManager>();
        services.AddScoped<IStudentSkillsService, StudentSkillsManager>();
        services.AddScoped<IStudentSocialMediasService, StudentSocialMediasManager>();
        services.AddScoped<IStudentStagesService, StudentStagesManager>();
        services.AddScoped<ISubTypesService, SubTypesManager>();
        services.AddScoped<ISurveysService, SurveysManager>();
        services.AddScoped<ITagsService, TagsManager>();
        services.AddScoped<IUserOperationClaimService, UserUserOperationClaimManager>();
        services.AddScoped<IOperationClaimService, OperationClaimManager>();
        services.AddScoped<IStudentSurveysService, StudentSurveysManager>();
        services.AddScoped<ICacheMemoryService, CacheService>();
        services.AddScoped<ILectureViewsService, LectureViewsManager>();
        services.AddScoped<IStudentPrivateCertificatesService, StudentPrivateCertificatesManager>();
        services.AddScoped<IOptionsService, OptionsManager>();
        services.AddScoped<IQuestionsService, QuestionsManager>();
        services.AddScoped<IQuestionOptionsService, QuestionOptionsManager>();
        services.AddScoped<IQuizsService, QuizsManager>();
        services.AddScoped<IQuizQuestionsService, QuizQuestionsManager>();
        services.AddScoped<IPoolsService, PoolsManager>();
        services.AddScoped<IPoolQuestionsService, PoolQuestionsManager>();
        services.AddScoped<IStudentQuizOptionsService, StudentQuizOptionsManager>();
        services.AddScoped<IGeneralQuizsService, GeneralQuizsManager>();
        services.AddScoped<IClassQuizsService, ClassQuizsManager>();
        services.AddScoped<IStudentQuizResultsService, StudentQuizResultsManager>();
        services.AddMemoryCache();
        return services;
    }

    public static IServiceCollection AddSubClassesOfType(
        this IServiceCollection services,
        Assembly assembly,
        Type type,
        Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}
