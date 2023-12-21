using Application.Features.ClassSurveys.Commands.Create;
using Application.Features.ClassSurveys.Commands.Delete;
using Application.Features.ClassSurveys.Commands.Update;
using Application.Features.ClassSurveys.Queries.GetById;
using Application.Features.ClassSurveys.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassSurveysController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateClassSurveyCommand createClassSurveyCommand)
    {
        CreatedClassSurveyResponse response = await Mediator.Send(createClassSurveyCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateClassSurveyCommand updateClassSurveyCommand)
    {
        UpdatedClassSurveyResponse response = await Mediator.Send(updateClassSurveyCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedClassSurveyResponse response = await Mediator.Send(new DeleteClassSurveyCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdClassSurveyResponse response = await Mediator.Send(new GetByIdClassSurveyQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListClassSurveyQuery getListClassSurveyQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListClassSurveyListItemDto> response = await Mediator.Send(getListClassSurveyQuery);
        return Ok(response);
    }
}