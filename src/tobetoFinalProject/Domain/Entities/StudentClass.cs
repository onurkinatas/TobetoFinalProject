using Core.Persistence.Repositories;

namespace Domain.Entities;

public class StudentClass : Entity<Guid>
{
    public string Name { get; set; }
    public virtual ICollection<ClassAnnouncement>? ClassAnnouncements { get; set; }
    public virtual ICollection<StudentClassStudent>? StudentClassStudentes { get; set; }
    public virtual ICollection<ClassQuiz>? ClassQuizs { get; set; }
    public virtual ICollection<ClassLecture>? ClassLectures { get; set; }
    public virtual ICollection<ClassSurvey>? ClassSurveys { get; set; }
    public virtual ICollection<ClassExam>? ClassExams { get; set; }

}


