using Application.Features.StudentLectureComments.Commands.Create;
using Application.Features.StudentLectureComments.Commands.Delete;
using Application.Features.StudentLectureComments.Commands.Update;
using Application.Features.StudentLectureComments.Queries.GetById;
using Application.Features.StudentLectureComments.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentLectureCommentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentLectureCommentCommand createStudentLectureCommentCommand)
    {
        CreatedStudentLectureCommentResponse response = await Mediator.Send(createStudentLectureCommentCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentLectureCommentCommand updateStudentLectureCommentCommand)
    {
        UpdatedStudentLectureCommentResponse response = await Mediator.Send(updateStudentLectureCommentCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedStudentLectureCommentResponse response = await Mediator.Send(new DeleteStudentLectureCommentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdStudentLectureCommentResponse response = await Mediator.Send(new GetByIdStudentLectureCommentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentLectureCommentQuery getListStudentLectureCommentQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentLectureCommentListItemDto> response = await Mediator.Send(getListStudentLectureCommentQuery);
        return Ok(response);
    }
}