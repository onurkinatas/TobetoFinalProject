using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentExams.Queries.GetListForLoggedStudent;
public class GetListStudentExamForLoggedStudentListItemDto : IDto
{
    public Guid ExamId { get; set; }
}
