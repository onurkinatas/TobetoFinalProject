using Core.Application.Dtos;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.LectureCompletionConditions.Queries.GetList;

public class GetListLectureCompletionConditionListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LectureId { get; set; }
    public int CompletionPercentage { get; set; }
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public string StudentEmail { get; set; }
    public string LectureName { get; set; }
    public string LectureImageUrl { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}