using Application.Features.StudentQuizOptions.Commands.Create;
using Application.Features.StudentQuizOptions.Commands.Delete;
using Application.Features.StudentQuizOptions.Commands.Update;
using Application.Features.StudentQuizOptions.Queries.GetById;
using Application.Features.StudentQuizOptions.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentQuizOptionsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentQuizOptionCommand createStudentQuizOptionCommand)
    {createStudentQuizOptionCommand.UserId = getUserIdFromRequest();    
        CreatedStudentQuizOptionResponse response = await Mediator.Send(createStudentQuizOptionCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentQuizOptionCommand updateStudentQuizOptionCommand)
    {
        UpdatedStudentQuizOptionResponse response = await Mediator.Send(updateStudentQuizOptionCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedStudentQuizOptionResponse response = await Mediator.Send(new DeleteStudentQuizOptionCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdStudentQuizOptionResponse response = await Mediator.Send(new GetByIdStudentQuizOptionQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentQuizOptionQuery getListStudentQuizOptionQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentQuizOptionListItemDto> response = await Mediator.Send(getListStudentQuizOptionQuery);
        return Ok(response);
    }
}