using Application.Features.Tags.Commands.Create;
using Application.Features.Tags.Commands.Delete;
using Application.Features.Tags.Commands.Update;
using Application.Features.Tags.Queries.GetById;
using Application.Features.Tags.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTagCommand createTagCommand)
    {
        CreatedTagResponse response = await Mediator.Send(createTagCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTagCommand updateTagCommand)
    {
        UpdatedTagResponse response = await Mediator.Send(updateTagCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedTagResponse response = await Mediator.Send(new DeleteTagCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdTagResponse response = await Mediator.Send(new GetByIdTagQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTagQuery getListTagQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListTagListItemDto> response = await Mediator.Send(getListTagQuery);
        return Ok(response);
    }
}