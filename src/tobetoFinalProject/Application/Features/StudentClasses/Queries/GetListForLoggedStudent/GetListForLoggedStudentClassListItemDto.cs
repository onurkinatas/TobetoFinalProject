using Application.Features.ClassAnnouncements.Queries.GetList;
using Application.Features.ClassLectures.Queries.GetList;
using Application.Features.ClassQuizs.Queries.GetList;
using Application.Features.ClassSurveys.Queries.GetList;
using Core.Application.Dtos;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentClasses.Queries.GetListForLoggedStudent;
public class GetListForLoggedStudentClassListItemDto:IDto
{
    public List<GetListClassAnnouncementListItemDto>? ClassAnnouncements { get; set; }
    public List<GetListClassQuizListItemDto>? ClassQuizs { get; set; }
    public List<GetListClassLectureListItemDto>? ClassLectures { get; set; }
    public List<GetListClassSurveyListItemDto>? ClassSurveys { get; set; }
    public int? ReadingAnnouncement { get; set; }
}

