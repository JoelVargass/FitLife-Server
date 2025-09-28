using Application.UseCases.Exercises.Common;
using Domain.Common.Errors;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using WebApi.Common.Controllers;
using Application.Interfaces.Repositories;
using Application.UseCases.Plans.Commands.Create;
using Application.UseCases.Plans.Commands.Delete;
using Application.UseCases.Plans.Commands.Update;
using Application.UseCases.Plans.Queries.ListPlans;

namespace WebApi.Controllers;

public class PlansController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IRepository<Plan> _repository;

    public PlansController(IMediator mediator, IMapper mapper, IRepository<Plan> repository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _repository = repository;
    }

    public record ListPlansRequest(int Page = 1, int PageSize = 10, string? Search = null);

    public record PlanCreateRequest(
        string Name,
        string? Description
    );

    public record PlanUpdateRequest(
        Guid Id,
        string Name,
        string? Description
    );

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] ListPlansRequest request)
    {
        var query = new ListPlansQuery(request.Page, request.PageSize, request.Search);
        var result = await _mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPlan(Guid id)
    {
        var plan = await _repository.GetByIdAsync(id);

        return plan is null 
            ? Problem(Errors.Plan.NotFound) 
            : Ok(_mapper.Map<ExerciseResult>(plan));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlan(PlanCreateRequest request)
    {
        var command = new CreatePlanCommand(
            request.Name,
            request.Description
        );

        var result = await _mediator.Send(command);

        return result.Match(_ => Ok(), Problem);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePlan(PlanUpdateRequest request)
    {
        var command = new UpdatePlanCommand(
            request.Id,
            request.Name,
            request.Description
        );

        var result = await _mediator.Send(command);

        return result.Match(_ => Ok(), Problem);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePlan(Guid id)
    {
        var command = new DeletePlanCommand(id);
        var result = await _mediator.Send(command);

        return result.Match(_ => Ok(), Problem);
    }
}
