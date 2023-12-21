using Application.Features.ClassLectures.Commands.Create;
using Application.Features.ClassLectures.Commands.Delete;
using Application.Features.ClassLectures.Commands.Update;
using Application.Features.ClassLectures.Queries.GetById;
using Application.Features.ClassLectures.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassLecturesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateClassLectureCommand createClassLectureCommand)
    {
        CreatedClassLectureResponse response = await Mediator.Send(createClassLectureCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateClassLectureCommand updateClassLectureCommand)
    {
        UpdatedClassLectureResponse response = await Mediator.Send(updateClassLectureCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedClassLectureResponse response = await Mediator.Send(new DeleteClassLectureCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdClassLectureResponse response = await Mediator.Send(new GetByIdClassLectureQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListClassLectureQuery getListClassLectureQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListClassLectureListItemDto> response = await Mediator.Send(getListClassLectureQuery);
        return Ok(response);
    }
}