using Application.Features.Questions.Queries.GetList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Quizs.Queries.GetQuizSession;
public class GetByIdQuizSessionResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public ICollection<GetListQuestionListItemDto> Questions { get; set; }
}

