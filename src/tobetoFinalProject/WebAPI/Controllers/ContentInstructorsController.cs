using Application.Features.ContentInstructors.Commands.Create;
using Application.Features.ContentInstructors.Commands.Delete;
using Application.Features.ContentInstructors.Commands.Update;
using Application.Features.ContentInstructors.Queries.GetById;
using Application.Features.ContentInstructors.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentInstructorsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContentInstructorCommand createContentInstructorCommand)
    {
        CreatedContentInstructorResponse response = await Mediator.Send(createContentInstructorCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContentInstructorCommand updateContentInstructorCommand)
    {
        UpdatedContentInstructorResponse response = await Mediator.Send(updateContentInstructorCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedContentInstructorResponse response = await Mediator.Send(new DeleteContentInstructorCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdContentInstructorResponse response = await Mediator.Send(new GetByIdContentInstructorQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContentInstructorQuery getListContentInstructorQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListContentInstructorListItemDto> response = await Mediator.Send(getListContentInstructorQuery);
        return Ok(response);
    }
}