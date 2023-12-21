using Application.Features.Announcements.Queries.GetList;
using Application.Features.Exams.Queries.GetList;
using Application.Features.Lectures.Queries.GetList;
using Application.Features.Students.Queries.GetList;
using Application.Features.Surveys.Queries.GetList;
using Core.Application.Dtos;

namespace Application.Features.StudentClasses.Queries.GetList;

public class GetListStudentClasses : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<GetListExamListItemDto> Exams { get; set; }
    public ICollection<GetListStudentListItemDto> Students { get; set; }
    public ICollection<GetListAnnouncementListItemDto> Announcements { get; set; }
    public ICollection<GetListSurveyListItemDto> Surveys { get; set; }
    public ICollection<GetListLectureListItemDto> Lectures { get; set; }
}