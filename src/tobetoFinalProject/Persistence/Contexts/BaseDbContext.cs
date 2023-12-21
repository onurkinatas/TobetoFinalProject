using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Domain.Entities;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<Appeal> Appeals { get; set; }
    public DbSet<AppealStage> AppealStages { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<ClassAnnouncement> ClassAnnouncements { get; set; }
    public DbSet<ClassExam> ClassExams { get; set; }
    public DbSet<ClassLecture> ClassLectures { get; set; }
    public DbSet<ClassSurvey> ClassSurveys { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<ContentCategory> ContentCategories { get; set; }
    public DbSet<ContentInstructor> ContentInstructors { get; set; }
    public DbSet<ContentLike> ContentLikes { get; set; }
    public DbSet<ContentTag> ContentTags { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseContent> CourseContents { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<LanguageLevel> LanguageLevels { get; set; }
    public DbSet<Lecture> Lectures { get; set; }
    public DbSet<LectureCompletionCondition> LectureCompletionConditions { get; set; }
    public DbSet<LectureCourse> LectureCourses { get; set; }
    public DbSet<LectureLike> LectureLikes { get; set; }
    public DbSet<LectureSpentTime> LectureSpentTimes { get; set; }
   // public DbSet<LectureView> LectureViews { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<SocialMedia> SocialMedias { get; set; }
    public DbSet<Stage> Stages { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentAnnouncement> StudentAnnouncements { get; set; }
    public DbSet<StudentAppeal> StudentAppeals { get; set; }
    public DbSet<StudentCertificate> StudentCertificates { get; set; }
    public DbSet<StudentClass> StudentClasses { get; set; }
    public DbSet<StudentClassStudent> StudentClassStudents { get; set; }
    public DbSet<StudentEducation> StudentEducations { get; set; }
    public DbSet<StudentExam> StudentExams { get; set; }
    public DbSet<StudentExperience> StudentExperiences { get; set; }
    public DbSet<StudentLanguageLevel> StudentLanguageLevels { get; set; }
    public DbSet<StudentSkill> StudentSkills { get; set; }
    public DbSet<StudentSocialMedia> StudentSocialMedias { get; set; }
    public DbSet<StudentStage> StudentStages { get; set; }
    public DbSet<SubType> SubTypes { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<StudentSurvey> StudentSurveys { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
        : base(dbContextOptions)
    {
        Configuration = configuration;
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) { 
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

       // modelBuilder.Entity<LectureView>().HasKey(x => new { x.StudentId, x.ContentId, x.LectureId});

       // modelBuilder.Entity<LectureView>()
            //.HasOne(x => x.Lecture)
           // .WithMany(z => z.lectu)
    }
}
