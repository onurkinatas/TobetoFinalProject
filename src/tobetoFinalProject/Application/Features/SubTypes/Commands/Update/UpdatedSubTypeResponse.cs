using Core.Application.Responses;

namespace Application.Features.SubTypes.Commands.Update;

public class UpdatedSubTypeResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}