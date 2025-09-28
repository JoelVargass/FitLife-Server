using Application.UseCases.Exercises.Commands.Create;
using Application.UseCases.Exercises.Commands.Delete;
using Application.UseCases.Exercises.Commands.Update;
using Application.UseCases.Exercises.Common;
using Application.UseCases.Exercises.Queries.ListExercises;
using Domain.Common.Errors;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using WebApi.Common.Controllers;
using Application.Interfaces.Repositories;

namespace WebApi.Controllers;

public class ExerciseController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IRepository<Exercise> _repository;

    public ExerciseController(IMediator mediator, IMapper mapper, IRepository<Exercise> repository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _repository = repository;
    }

    public record ListExercisesRequest(int Page = 1, int PageSize = 10, string? Search = null);

    public record ExerciseCreateRequest(
        string Name,
        MuscleType MuscleType,
        string? Description,
        string? Duration
    );

    public record ExerciseUpdateRequest(
        Guid Id,
        string Name,
        MuscleType MuscleType,
        string? Description,
        string? Duration
    );

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] ListExercisesRequest request)
    {
        var query = new ListExercisesQuery(request.Page, request.PageSize, request.Search);
        var result = await _mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetExercise(Guid id)
    {
        var exercise = await _repository.GetByIdAsync(id);

        return exercise is null 
            ? Problem(Errors.Exercise.NotFound) 
            : Ok(_mapper.Map<ExerciseResult>(exercise));
    }

    [HttpPost]
    public async Task<IActionResult> CreateExercise(ExerciseCreateRequest request)
    {
        var command = new CreateExerciseCommand(
            request.Name,
            request.MuscleType,
            request.Description,
            request.Duration
        );

        var result = await _mediator.Send(command);

        return result.Match(_ => Ok(), Problem);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateExercise(ExerciseUpdateRequest request)
    {
        var command = new UpdateExerciseCommand(
            request.Id,
            request.Name,
            request.MuscleType,
            request.Description,
            request.Duration
        );

        var result = await _mediator.Send(command);

        return result.Match(_ => Ok(), Problem);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteExercise(Guid id)
    {
        var command = new DeleteExerciseCommand(id);
        var result = await _mediator.Send(command);

        return result.Match(_ => Ok(), Problem);
    }
}
