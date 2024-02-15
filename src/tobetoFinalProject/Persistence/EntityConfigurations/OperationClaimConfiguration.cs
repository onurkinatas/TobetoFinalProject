using Application.Features.OperationClaims.Constants;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasMany(oc => oc.UserOperationClaims);

        builder.HasData(getSeeds());
    }

    private HashSet<OperationClaim> getSeeds()
    {
        int id = 0;
        HashSet<OperationClaim> seeds =
            new()
            {
                new OperationClaim { Id = ++id, Name = GeneralOperationClaims.Admin }
            };

        
        #region Announcements
        seeds.Add(new OperationClaim { Id = ++id, Name = "Announcements.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Announcements.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Announcements.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Announcements.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Announcements.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Announcements.Delete" });
        #endregion
        #region Appeals
        seeds.Add(new OperationClaim { Id = ++id, Name = "Appeals.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Appeals.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Appeals.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Appeals.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Appeals.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Appeals.Delete" });
        #endregion

        #region AppealStages
        seeds.Add(new OperationClaim { Id = ++id, Name = "AppealStages.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "AppealStages.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "AppealStages.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "AppealStages.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "AppealStages.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "AppealStages.Delete" });
        #endregion

        #region Categories
        seeds.Add(new OperationClaim { Id = ++id, Name = "Categories.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Categories.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Categories.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Categories.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Categories.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Categories.Delete" });
        #endregion

        #region Certificates
        seeds.Add(new OperationClaim { Id = ++id, Name = "Certificates.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Certificates.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Certificates.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Certificates.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Certificates.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Certificates.Delete" });
        #endregion

        #region Cities
        seeds.Add(new OperationClaim { Id = ++id, Name = "Cities.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Cities.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Cities.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Cities.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Cities.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Cities.Delete" });
        #endregion

        #region ClassAnnouncements
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassAnnouncements.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassAnnouncements.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassAnnouncements.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassAnnouncements.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassAnnouncements.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassAnnouncements.Delete" });
        #endregion

        #region ClassExams
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassExams.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassExams.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassExams.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassExams.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassExams.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassExams.Delete" });
        #endregion

        #region ClassLectures
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassLectures.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassLectures.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassLectures.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassLectures.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassLectures.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassLectures.Delete" });
        #endregion

        #region ClassSurveys
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassSurveys.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassSurveys.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassSurveys.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassSurveys.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassSurveys.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ClassSurveys.Delete" });
        #endregion

        #region Contents
        seeds.Add(new OperationClaim { Id = ++id, Name = "Contents.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Contents.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Contents.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Contents.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Contents.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Contents.Delete" });
        #endregion

        #region ContentCategories
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentCategories.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentCategories.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentCategories.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentCategories.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentCategories.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentCategories.Delete" });
        #endregion

        #region ContentInstructors
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentInstructors.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentInstructors.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentInstructors.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentInstructors.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentInstructors.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentInstructors.Delete" });
        #endregion

        #region ContentLikes
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentLikes.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentLikes.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentLikes.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentLikes.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentLikes.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentLikes.Delete" });
        #endregion

        #region ContentTags
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentTags.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentTags.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentTags.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentTags.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentTags.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "ContentTags.Delete" });
        #endregion

        #region Courses
        seeds.Add(new OperationClaim { Id = ++id, Name = "Courses.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Courses.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Courses.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Courses.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Courses.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Courses.Delete" });
        #endregion

        #region CourseContents
        seeds.Add(new OperationClaim { Id = ++id, Name = "CourseContents.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "CourseContents.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "CourseContents.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "CourseContents.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "CourseContents.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "CourseContents.Delete" });
        #endregion

        #region Districts
        seeds.Add(new OperationClaim { Id = ++id, Name = "Districts.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Districts.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Districts.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Districts.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Districts.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Districts.Delete" });
        #endregion

        #region Exams
        seeds.Add(new OperationClaim { Id = ++id, Name = "Exams.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Exams.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Exams.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Exams.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Exams.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Exams.Delete" });
        #endregion

        #region Instructors
        seeds.Add(new OperationClaim { Id = ++id, Name = "Instructors.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Instructors.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Instructors.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Instructors.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Instructors.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Instructors.Delete" });
        #endregion

        #region Languages
        seeds.Add(new OperationClaim { Id = ++id, Name = "Languages.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Languages.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Languages.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Languages.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Languages.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Languages.Delete" });
        #endregion

        #region LanguageLevels
        seeds.Add(new OperationClaim { Id = ++id, Name = "LanguageLevels.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LanguageLevels.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LanguageLevels.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LanguageLevels.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LanguageLevels.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LanguageLevels.Delete" });
        #endregion

        #region Lectures
        seeds.Add(new OperationClaim { Id = ++id, Name = "Lectures.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Lectures.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Lectures.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Lectures.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Lectures.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Lectures.Delete" });
        #endregion

        #region LectureCompletionConditions
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureCompletionConditions.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureCompletionConditions.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureCompletionConditions.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureCompletionConditions.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureCompletionConditions.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureCompletionConditions.Delete" });
        #endregion

        #region LectureCourses
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureCourses.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureCourses.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureCourses.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureCourses.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureCourses.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureCourses.Delete" });
        #endregion

        #region LectureLikes
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureLikes.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureLikes.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureLikes.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureLikes.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureLikes.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureLikes.Delete" });
        #endregion

        #region LectureSpentTimes
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureSpentTimes.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureSpentTimes.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureSpentTimes.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureSpentTimes.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureSpentTimes.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureSpentTimes.Delete" });
        #endregion

        #region LectureViews
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureViews.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureViews.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureViews.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureViews.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureViews.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "LectureViews.Delete" });
        #endregion

        #region Manufacturers
        seeds.Add(new OperationClaim { Id = ++id, Name = "Manufacturers.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Manufacturers.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Manufacturers.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Manufacturers.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Manufacturers.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Manufacturers.Delete" });
        #endregion

        #region Skills
        seeds.Add(new OperationClaim { Id = ++id, Name = "Skills.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Skills.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Skills.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Skills.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Skills.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Skills.Delete" });
        #endregion

        #region SocialMedias
        seeds.Add(new OperationClaim { Id = ++id, Name = "SocialMedias.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SocialMedias.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SocialMedias.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SocialMedias.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SocialMedias.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SocialMedias.Delete" });
        #endregion

        #region Stages
        seeds.Add(new OperationClaim { Id = ++id, Name = "Stages.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Stages.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Stages.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Stages.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Stages.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Stages.Delete" });
        #endregion

        #region Students
        seeds.Add(new OperationClaim { Id = ++id, Name = "Students.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Students.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Students.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Students.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Students.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Students.Delete" });
        #endregion

        #region StudentAnnouncements
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentAnnouncements.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentAnnouncements.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentAnnouncements.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentAnnouncements.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentAnnouncements.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentAnnouncements.Delete" });
        #endregion

        #region StudentAppeals
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentAppeals.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentAppeals.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentAppeals.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentAppeals.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentAppeals.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentAppeals.Delete" });
        #endregion

        #region StudentCertificates
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentCertificates.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentCertificates.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentCertificates.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentCertificates.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentCertificates.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentCertificates.Delete" });
        #endregion

        #region StudentClasses
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentClasses.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentClasses.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentClasses.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentClasses.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentClasses.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentClasses.Delete" });
        #endregion

        #region StudentClassStudents
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentClassStudents.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentClassStudents.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentClassStudents.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentClassStudents.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentClassStudents.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentClassStudents.Delete" });
        #endregion

        #region StudentEducations
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentEducations.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentEducations.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentEducations.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentEducations.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentEducations.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentEducations.Delete" });
        #endregion

        #region StudentExams
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentExams.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentExams.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentExams.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentExams.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentExams.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentExams.Delete" });
        #endregion

        #region StudentExperiences
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentExperiences.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentExperiences.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentExperiences.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentExperiences.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentExperiences.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentExperiences.Delete" });
        #endregion

        #region StudentLanguageLevels
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentLanguageLevels.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentLanguageLevels.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentLanguageLevels.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentLanguageLevels.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentLanguageLevels.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentLanguageLevels.Delete" });
        #endregion

        #region StudentSkills
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSkills.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSkills.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSkills.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSkills.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSkills.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSkills.Delete" });
        #endregion

        #region StudentSocialMedias
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSocialMedias.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSocialMedias.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSocialMedias.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSocialMedias.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSocialMedias.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSocialMedias.Delete" });
        #endregion

        #region StudentStages
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentStages.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentStages.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentStages.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentStages.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentStages.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentStages.Delete" });
        #endregion

        #region SubTypes
        seeds.Add(new OperationClaim { Id = ++id, Name = "SubTypes.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SubTypes.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SubTypes.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SubTypes.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SubTypes.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "SubTypes.Delete" });
        #endregion

        #region Surveys
        seeds.Add(new OperationClaim { Id = ++id, Name = "Surveys.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Surveys.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Surveys.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Surveys.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Surveys.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Surveys.Delete" });
        #endregion

        #region Tags
        seeds.Add(new OperationClaim { Id = ++id, Name = "Tags.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Tags.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Tags.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Tags.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Tags.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Tags.Delete" });
        #endregion

        #region StudentSurveys
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSurveys.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSurveys.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSurveys.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSurveys.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSurveys.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentSurveys.Delete" });
        #endregion

        #region StudentPrivateCertificates
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentPrivateCertificates.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentPrivateCertificates.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentPrivateCertificates.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentPrivateCertificates.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentPrivateCertificates.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "StudentPrivateCertificates.Delete" });
        #endregion


        #region Options
        seeds.Add(new OperationClaim { Id = ++id, Name = "Options.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Options.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Options.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Options.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Options.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Options.Delete" });
        #endregion
        #region Questions
        seeds.Add(new OperationClaim { Id = ++id, Name = "Questions.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Questions.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Questions.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Questions.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Questions.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Questions.Delete" });
        #endregion
        #region QuestionOptions
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuestionOptions.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuestionOptions.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuestionOptions.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuestionOptions.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuestionOptions.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuestionOptions.Delete" });
        #endregion
        #region Quizs
        seeds.Add(new OperationClaim { Id = ++id, Name = "Quizs.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Quizs.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Quizs.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Quizs.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Quizs.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Quizs.Delete" });
        #endregion
        #region QuizQuestions
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuizQuestions.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuizQuestions.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuizQuestions.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuizQuestions.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuizQuestions.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "QuizQuestions.Delete" });
        #endregion

        #region Pools
        seeds.Add(new OperationClaim { Id = ++id, Name = "Pools.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Pools.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Pools.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Pools.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Pools.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Pools.Delete" });
        #endregion
        #region PoolQuestions
        seeds.Add(new OperationClaim { Id = ++id, Name = "PoolQuestions.Admin" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PoolQuestions.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PoolQuestions.Write" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PoolQuestions.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PoolQuestions.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "PoolQuestions.Delete" });
        #endregion
        return seeds;
    }
}
