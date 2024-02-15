using Application.Features.Questions.Queries.GetList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Quizs.Queries.GetQuizSession;
public class GetByIdQuizSessionResponse
{
    public ICollection<GetListQuestionListItemDto> Questions { get; set; }
}

