//using Application.UseCases.ExercisePlans.Commands.Delete;
//using Application.UseCases.ExercisePlans.Commands.Update;
using Application.UseCases.ExercisePlans.Common;
using Application.UseCases.ExercisePlans.Queries.ListExercisePlans;
using Domain.Common.Errors;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using WebApi.Common.Controllers;
using Application.Interfaces.Repositories;
using Application.UseCases.ExercisePlans.Commands;

namespace WebApi.Controllers;

public class ExercisePlansController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IRepository<ExercisePlan> _repository;

    public ExercisePlansController(IMediator mediator, IMapper mapper, IRepository<ExercisePlan> repository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _repository = repository;
    }

    public record ListExercisePlansRequest(int Page = 1, int PageSize = 10);

    public record ExercisePlanCreateRequest(
        Guid PlanId,
        Guid ExerciseId,
        DayOfWeek DayOfWeek,
        int Series,
        int Repetitions
    );
/*
    public record ExercisePlanUpdateRequest(
        Guid Id,
        Guid PlanId,
        Guid ExerciseId,
        DayOfWeek DayOfWeek,
        int Series,
        int Repetitions
    );
*/
    [HttpGet]
    public async Task<IActionResult> List([FromQuery] ListExercisePlansRequest request)
    {
        var query = new ListExercisePlansQuery(request.Page, request.PageSize);
        var result = await _mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetExercisePlan(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        return entity is null
            ? Problem(Errors.Plan.NotFound)
            : Ok(_mapper.Map<ExercisePlanResult>(entity));
    }

    [HttpPost]
    public async Task<IActionResult> CreateExercisePlan(ExercisePlanCreateRequest request)
    {
        var command = new CreateExercisePlanCommand(
            request.PlanId,
            request.ExerciseId,
            request.DayOfWeek,
            request.Series,
            request.Repetitions
        );

        var result = await _mediator.Send(command);

        return result.Match(_ => Ok(), Problem);
    }
/*
    [HttpPut]
    public async Task<IActionResult> UpdateExercisePlan(ExercisePlanUpdateRequest request)
    {
        var command = new UpdateExercisePlanCommand(
            request.Id,
            request.PlanId,
            request.ExerciseId,
            request.DayOfWeek,
            request.Series,
            request.Repetitions
        );

        var result = await _mediator.Send(command);

        return result.Match(_ => Ok(), Problem);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteExercisePlan(Guid id)
    {
        var command = new DeleteExercisePlanCommand(id);
        var result = await _mediator.Send(command);

        return result.Match(_ => Ok(), Problem);
    } */
}
