using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentExams.Queries.GetByExamIdForLoggedStudent;
public class GetByExamIdForLoggedStudentQueryResponse : IResponse
{
    public bool IsJoined { get; set; }
}

