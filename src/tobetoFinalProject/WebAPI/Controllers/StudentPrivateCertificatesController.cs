using Application.Features.StudentPrivateCertificates.Commands.Create;
using Application.Features.StudentPrivateCertificates.Commands.Delete;
using Application.Features.StudentPrivateCertificates.Commands.Update;
using Application.Features.StudentPrivateCertificates.Queries.GetById;
using Application.Features.StudentPrivateCertificates.Queries.GetList;
using Application.Features.StudentPrivateCertificates.Queries.GetListForLoggedStudent;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Security.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentPrivateCertificatesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] CreateStudentPrivateCertificateCommand createStudentPrivateCertificateCommand)
    {
        createStudentPrivateCertificateCommand.UserId = getUserIdFromRequest();
        CreatedStudentPrivateCertificateResponse response = await Mediator.Send(createStudentPrivateCertificateCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentPrivateCertificateCommand updateStudentPrivateCertificateCommand)
    {
        UpdatedStudentPrivateCertificateResponse response = await Mediator.Send(updateStudentPrivateCertificateCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStudentPrivateCertificateResponse response = await Mediator.Send(new DeleteStudentPrivateCertificateCommand { Id = id,UserId= getUserIdFromRequest() });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStudentPrivateCertificateResponse response = await Mediator.Send(new GetByIdStudentPrivateCertificateQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentPrivateCertificateQuery getListStudentPrivateCertificateQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentPrivateCertificateListItemDto> response = await Mediator.Send(getListStudentPrivateCertificateQuery);
        return Ok(response);
    }
    [HttpGet("GetForLoggedStudent")]
    public async Task<IActionResult> GetForLoggedStudentList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentPrivateCertificateForLoggedStudentQuery getListStudentPrivateCertificateQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentPrivateCertificateListItemDto> response = await Mediator.Send(getListStudentPrivateCertificateQuery);
        return Ok(response);
    }
}