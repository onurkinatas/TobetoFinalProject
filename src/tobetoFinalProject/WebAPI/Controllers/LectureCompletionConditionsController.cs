using Application.Features.LectureCompletionConditions.Commands.Create;
using Application.Features.LectureCompletionConditions.Commands.Delete;
using Application.Features.LectureCompletionConditions.Commands.Update;
using Application.Features.LectureCompletionConditions.Queries.GetById;
using Application.Features.LectureCompletionConditions.Queries.GetForLoggedStudent;
using Application.Features.LectureCompletionConditions.Queries.GetList;
using Application.Features.LectureCompletionConditions.Queries.GetListByLectureId;
using Application.Features.LectureCompletionConditions.Queries.GetListForContiuned;
using Application.Features.LectureCompletionConditions.Queries.GetListLecturesForCompleted;
using Core.Application.Requests;
using Core.Application.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LectureCompletionConditionsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateLectureCompletionConditionCommand createLectureCompletionConditionCommand)
    {
        CreatedLectureCompletionConditionResponse response = await Mediator.Send(createLectureCompletionConditionCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLectureCompletionConditionCommand updateLectureCompletionConditionCommand)
    {
        UpdatedLectureCompletionConditionResponse response = await Mediator.Send(updateLectureCompletionConditionCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedLectureCompletionConditionResponse response = await Mediator.Send(new DeleteLectureCompletionConditionCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdLectureCompletionConditionResponse response = await Mediator.Send(new GetByIdLectureCompletionConditionQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListLectureCompletionConditionQuery getListLectureCompletionConditionQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListLectureCompletionConditionListItemDto> response = await Mediator.Send(getListLectureCompletionConditionQuery);
        return Ok(response);
    }
    [HttpGet("getListWithLectureId{lectureId}")]
    public async Task<IActionResult> GetListByLectureId([FromQuery] PageRequest pageRequest, [FromRoute] Guid lectureId)
    {
        GetListByLectureIdLectureCompletionConditionQuery getListByLectureIdLectureCompletionConditionQuery = new() { PageRequest = pageRequest,LectureId=lectureId };
        GetListResponse<GetListLectureCompletionConditionListItemDto> response = await Mediator.Send(getListByLectureIdLectureCompletionConditionQuery);
        return Ok(response);
    }

    [HttpGet("getByLectureId{lectureId}")]
    public async Task<IActionResult> GetByLectureId([FromRoute] Guid lectureId)
    {
        GetByLoggedStudentCompletionConditionResponse response = await Mediator.Send(new GetByLoggedStudentCompletionConditionQuery { LectureId=lectureId });
        return Ok(response);
    }
    [HttpGet("getListForCompleted")]
    public async Task<IActionResult> GetListForCompleted([FromQuery] PageRequest pageRequest)
    {
        GetListLectureCompletionConditionForCompletedQuery getListByLectureIdLectureCompletionConditionQuery = new() { PageRequest = pageRequest, UserId = getUserIdFromRequest() };
        GetListResponse<GetListLectureCompletionConditionListItemDto> response = await Mediator.Send(getListByLectureIdLectureCompletionConditionQuery);
        return Ok(response);
    }
    [HttpGet("getListForContinued")]
    public async Task<IActionResult> GetListForContinued([FromQuery] PageRequest pageRequest)
    {
        GetListLectureCompletionConditionForContiuned getListByLectureIdLectureCompletionConditionQuery = new() { PageRequest = pageRequest,UserId=getUserIdFromRequest() };
        GetListResponse<GetListLectureCompletionConditionListItemDto> response = await Mediator.Send(getListByLectureIdLectureCompletionConditionQuery);
        return Ok(response);
    }
}