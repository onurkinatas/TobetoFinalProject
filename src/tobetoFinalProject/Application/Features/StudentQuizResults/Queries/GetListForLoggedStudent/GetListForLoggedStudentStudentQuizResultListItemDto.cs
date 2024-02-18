using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentQuizResults.Queries.GetListForLoggedStudent;
public class GetListForLoggedStudentStudentQuizResultListItemDto : IDto
{
    public int Id { get; set; }
    public int QuizId { get; set; }
}
