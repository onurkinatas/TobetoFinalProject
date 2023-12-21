using Application.Features.Lectures.Commands.Create;
using Application.Features.Lectures.Commands.Delete;
using Application.Features.Lectures.Commands.Update;
using Application.Features.Lectures.Queries.GetById;
using Application.Features.Lectures.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LecturesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateLectureCommand createLectureCommand)
    {
        CreatedLectureResponse response = await Mediator.Send(createLectureCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLectureCommand updateLectureCommand)
    {
        UpdatedLectureResponse response = await Mediator.Send(updateLectureCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedLectureResponse response = await Mediator.Send(new DeleteLectureCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdLectureResponse response = await Mediator.Send(new GetByIdLectureQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListLectureQuery getListLectureQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListLectureListItemDto> response = await Mediator.Send(getListLectureQuery);
        return Ok(response);
    }
}