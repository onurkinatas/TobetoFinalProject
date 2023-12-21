using Core.Application.Responses;

namespace Application.Features.Contents.Commands.Update;

public class UpdatedContentResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid LanguageId { get; set; }
    public Guid SubTypeId { get; set; }
    public string VideoUrl { get; set; }
    public string Description { get; set; }
    public Guid ManufacturerId { get; set; }
    public int Duration { get; set; }
    public Guid? ContentCategoryId { get; set; }
}