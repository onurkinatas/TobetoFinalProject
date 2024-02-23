
using Application.Features.Quizs.Queries.Dtos;
using Core.Application.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Quizs.Queries.GetQuizDeatailByQuizIdForLoggedStudent;
public class GetQuizDetailByQuizIdForLoggedStudentResponse:IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public ICollection<GetListQuestionListItemDto>? Questions { get; set; }
    public ICollection<StudentQuizOption>? StudentOptions { get; set; }
}

