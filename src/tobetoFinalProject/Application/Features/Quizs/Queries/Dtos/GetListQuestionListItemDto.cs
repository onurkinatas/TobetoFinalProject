using Application.Features.QuestionOptions.Queries.GetList;
using Core.Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Quizs.Queries.Dtos;
public class GetListQuestionListItemDto : IDto
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }
    public string Sentence { get; set; }
    public int CorrectOptionId { get; set; }
    public ICollection<GetListQuestionOptionListItemDto>? QuestionOptions { get; set; }
}
