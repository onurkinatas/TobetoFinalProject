using Application.Features.LectureCourses.Commands.Create;
using Application.Features.LectureCourses.Commands.Delete;
using Application.Features.LectureCourses.Commands.Update;
using Application.Features.LectureCourses.Queries.GetById;
using Application.Features.LectureCourses.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LectureCoursesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateLectureCourseCommand createLectureCourseCommand)
    {
        CreatedLectureCourseResponse response = await Mediator.Send(createLectureCourseCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLectureCourseCommand updateLectureCourseCommand)
    {
        UpdatedLectureCourseResponse response = await Mediator.Send(updateLectureCourseCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedLectureCourseResponse response = await Mediator.Send(new DeleteLectureCourseCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdLectureCourseResponse response = await Mediator.Send(new GetByIdLectureCourseQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListLectureCourseQuery getListLectureCourseQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListLectureCourseListItemDto> response = await Mediator.Send(getListLectureCourseQuery);
        return Ok(response);
    }
}