using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.StudentQuizOptions.Commands.Create;

public class CreatedStudentQuizOptionResponse : IResponse
{
    public List<StudentQuizOption> StudentQuizOptions { get; set; }
}