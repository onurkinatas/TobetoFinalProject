using Application.Features.Announcements.Queries.GetList;
using Application.Features.Exams.Queries.GetList;
using Application.Features.Lectures.Queries.GetList;
using Application.Features.Surveys.Queries.GetList;
using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Students.Queries.GetList;
public class GetStudentClassesDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<GetListExamListItemDto> Exams { get; set; }
    public ICollection<GetListAnnouncementListItemDto> Announcements { get; set; }
    public ICollection<GetListSurveyListItemDto> Surveys { get; set; }
    public ICollection<GetListLectureListItemDto> Lectures { get; set; }
}
