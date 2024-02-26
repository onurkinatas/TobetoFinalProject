using Application.Features.CommentSubComments.Commands.Create;
using Application.Features.CommentSubComments.Commands.Delete;
using Application.Features.CommentSubComments.Commands.Update;
using Application.Features.CommentSubComments.Queries.GetById;
using Application.Features.CommentSubComments.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentSubCommentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCommentSubCommentCommand createCommentSubCommentCommand)
    {
        CreatedCommentSubCommentResponse response = await Mediator.Send(createCommentSubCommentCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCommentSubCommentCommand updateCommentSubCommentCommand)
    {
        UpdatedCommentSubCommentResponse response = await Mediator.Send(updateCommentSubCommentCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedCommentSubCommentResponse response = await Mediator.Send(new DeleteCommentSubCommentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdCommentSubCommentResponse response = await Mediator.Send(new GetByIdCommentSubCommentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCommentSubCommentQuery getListCommentSubCommentQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCommentSubCommentListItemDto> response = await Mediator.Send(getListCommentSubCommentQuery);
        return Ok(response);
    }
}