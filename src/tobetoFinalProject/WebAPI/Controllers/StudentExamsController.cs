using Application.Features.StudentExams.Commands.Create;
using Application.Features.StudentExams.Commands.Delete;
using Application.Features.StudentExams.Commands.Update;
using Application.Features.StudentExams.Queries.GetByExamIdForLoggedStudent;
using Application.Features.StudentExams.Queries.GetById;
using Application.Features.StudentExams.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentExamsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentExamCommand createStudentExamCommand)
    {
        CreatedStudentExamResponse response = await Mediator.Send(createStudentExamCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentExamCommand updateStudentExamCommand)
    {
        UpdatedStudentExamResponse response = await Mediator.Send(updateStudentExamCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStudentExamResponse response = await Mediator.Send(new DeleteStudentExamCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStudentExamResponse response = await Mediator.Send(new GetByIdStudentExamQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentExamQuery getListStudentExamQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentExamListItemDto> response = await Mediator.Send(getListStudentExamQuery);
        return Ok(response);
    }
    [HttpGet("{examId}")]
    public async Task<IActionResult> GetByExamId([FromRoute] Guid examId)
    {
        GetByExamIdForLoggedStudentQueryResponse response = await Mediator.Send(new GetByExamIdForLoggedStudentQuery { ExamId = examId });
        return Ok(response);
    }
}