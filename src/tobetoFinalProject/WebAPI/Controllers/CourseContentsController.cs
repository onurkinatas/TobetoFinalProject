using Application.Features.CourseContents.Commands.Create;
using Application.Features.CourseContents.Commands.Delete;
using Application.Features.CourseContents.Commands.Update;
using Application.Features.CourseContents.Queries.GetById;
using Application.Features.CourseContents.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseContentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCourseContentCommand createCourseContentCommand)
    {
        CreatedCourseContentResponse response = await Mediator.Send(createCourseContentCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCourseContentCommand updateCourseContentCommand)
    {
        UpdatedCourseContentResponse response = await Mediator.Send(updateCourseContentCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedCourseContentResponse response = await Mediator.Send(new DeleteCourseContentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdCourseContentResponse response = await Mediator.Send(new GetByIdCourseContentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCourseContentQuery getListCourseContentQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCourseContentListItemDto> response = await Mediator.Send(getListCourseContentQuery);
        return Ok(response);
    }
}