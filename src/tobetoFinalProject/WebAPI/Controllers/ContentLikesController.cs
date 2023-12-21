using Application.Features.ContentLikes.Commands.Create;
using Application.Features.ContentLikes.Commands.Delete;
using Application.Features.ContentLikes.Commands.Update;
using Application.Features.ContentLikes.Queries.GetById;
using Application.Features.ContentLikes.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentLikesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContentLikeCommand createContentLikeCommand)
    {
        CreatedContentLikeResponse response = await Mediator.Send(createContentLikeCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContentLikeCommand updateContentLikeCommand)
    {
        UpdatedContentLikeResponse response = await Mediator.Send(updateContentLikeCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedContentLikeResponse response = await Mediator.Send(new DeleteContentLikeCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdContentLikeResponse response = await Mediator.Send(new GetByIdContentLikeQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContentLikeQuery getListContentLikeQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListContentLikeListItemDto> response = await Mediator.Send(getListContentLikeQuery);
        return Ok(response);
    }
}