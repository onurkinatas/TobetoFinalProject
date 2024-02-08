using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentSurveys.Queries.GetListForLoggedStudent;
public class GetListStudentSurveyForLoggedStudentListItemDto:IDto
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public DateTime SurveyStartDate { get; set; }
    public DateTime SurveyEndDate { get; set; }
    public string SurveyName { get; set; }
    public string SurveyUrl { get; set; }
    public string SurveyDescription { get; set; }
}

