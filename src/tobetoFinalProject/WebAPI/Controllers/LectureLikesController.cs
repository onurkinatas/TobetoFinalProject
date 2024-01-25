using Application.Features.LectureLikes.Commands.Create;
using Application.Features.LectureLikes.Commands.Delete;
using Application.Features.LectureLikes.Commands.Update;
using Application.Features.LectureLikes.Queries.GetById;
using Application.Features.LectureLikes.Queries.GetByLectureId;
using Application.Features.LectureLikes.Queries.GetList;
using Application.Features.LectureLikes.Queries.GetListForLoggedStudent;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LectureLikesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateLectureLikeCommand createLectureLikeCommand)
    {
        CreatedLectureLikeResponse response = await Mediator.Send(createLectureLikeCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLectureLikeCommand updateLectureLikeCommand)
    {
        UpdatedLectureLikeResponse response = await Mediator.Send(updateLectureLikeCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedLectureLikeResponse response = await Mediator.Send(new DeleteLectureLikeCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdLectureLikeResponse response = await Mediator.Send(new GetByIdLectureLikeQuery { Id = id });
        return Ok(response);
    }
    [HttpGet("getByLectureId{lectureId}")]
    public async Task<IActionResult> GetByLectureId([FromRoute] Guid lectureId)
    {
        GetByLectureIdLectureLikeResponse response = await Mediator.Send(new GetByLectureIdLectureLikeQuery { LectureId = lectureId });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListLectureLikeQuery getListLectureLikeQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListLectureLikeListItemDto> response = await Mediator.Send(getListLectureLikeQuery);
        return Ok(response);
    }
    [HttpGet("GetListForActiveStudent")]
    public async Task<IActionResult> GetListForActiveStudent([FromQuery] PageRequest pageRequest)
    {
        GetListLectureLikeForLoggedStudentQuery getListLectureLikeForLoggedStudentQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListLectureLikeForLoggedStudentListItemDto> response = await Mediator.Send(getListLectureLikeForLoggedStudentQuery);
        return Ok(response);
    }
}