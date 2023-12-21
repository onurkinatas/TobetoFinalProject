using Application.Features.LectureSpentTimes.Commands.Create;
using Application.Features.LectureSpentTimes.Commands.Delete;
using Application.Features.LectureSpentTimes.Commands.Update;
using Application.Features.LectureSpentTimes.Queries.GetById;
using Application.Features.LectureSpentTimes.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LectureSpentTimesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateLectureSpentTimeCommand createLectureSpentTimeCommand)
    {
        CreatedLectureSpentTimeResponse response = await Mediator.Send(createLectureSpentTimeCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLectureSpentTimeCommand updateLectureSpentTimeCommand)
    {
        UpdatedLectureSpentTimeResponse response = await Mediator.Send(updateLectureSpentTimeCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedLectureSpentTimeResponse response = await Mediator.Send(new DeleteLectureSpentTimeCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdLectureSpentTimeResponse response = await Mediator.Send(new GetByIdLectureSpentTimeQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListLectureSpentTimeQuery getListLectureSpentTimeQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListLectureSpentTimeListItemDto> response = await Mediator.Send(getListLectureSpentTimeQuery);
        return Ok(response);
    }
}