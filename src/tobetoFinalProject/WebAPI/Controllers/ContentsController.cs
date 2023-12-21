using Application.Features.Contents.Commands.Create;
using Application.Features.Contents.Commands.Delete;
using Application.Features.Contents.Commands.Update;
using Application.Features.Contents.Queries.GetById;
using Application.Features.Contents.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContentCommand createContentCommand)
    {
        CreatedContentResponse response = await Mediator.Send(createContentCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContentCommand updateContentCommand)
    {
        UpdatedContentResponse response = await Mediator.Send(updateContentCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedContentResponse response = await Mediator.Send(new DeleteContentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdContentResponse response = await Mediator.Send(new GetByIdContentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContentQuery getListContentQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListContentListItemDto> response = await Mediator.Send(getListContentQuery);
        return Ok(response);
    }
}