using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LectureCompletionConditions.Queries.GetForLoggedStudent;
public class GetByLoggedStudentCompletionConditionResponse: IResponse
{
    public int Percentage { get; set; }
}

