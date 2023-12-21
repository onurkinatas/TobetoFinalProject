using Application.Features.ContentTags.Commands.Create;
using Application.Features.ContentTags.Commands.Delete;
using Application.Features.ContentTags.Commands.Update;
using Application.Features.ContentTags.Queries.GetById;
using Application.Features.ContentTags.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentTagsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContentTagCommand createContentTagCommand)
    {
        CreatedContentTagResponse response = await Mediator.Send(createContentTagCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContentTagCommand updateContentTagCommand)
    {
        UpdatedContentTagResponse response = await Mediator.Send(updateContentTagCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedContentTagResponse response = await Mediator.Send(new DeleteContentTagCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdContentTagResponse response = await Mediator.Send(new GetByIdContentTagQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContentTagQuery getListContentTagQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListContentTagListItemDto> response = await Mediator.Send(getListContentTagQuery);
        return Ok(response);
    }
}