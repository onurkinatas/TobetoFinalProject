using Application.Features.StudentCertificates.Commands.Create;
using Application.Features.StudentCertificates.Commands.Delete;
using Application.Features.StudentCertificates.Commands.Update;
using Application.Features.StudentCertificates.Queries.GetById;
using Application.Features.StudentCertificates.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentCertificatesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateStudentCertificateCommand createStudentCertificateCommand)
    {
        CreatedStudentCertificateResponse response = await Mediator.Send(createStudentCertificateCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateStudentCertificateCommand updateStudentCertificateCommand)
    {
        UpdatedStudentCertificateResponse response = await Mediator.Send(updateStudentCertificateCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedStudentCertificateResponse response = await Mediator.Send(new DeleteStudentCertificateCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdStudentCertificateResponse response = await Mediator.Send(new GetByIdStudentCertificateQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListStudentCertificateQuery getListStudentCertificateQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListStudentCertificateListItemDto> response = await Mediator.Send(getListStudentCertificateQuery);
        return Ok(response);
    }
}