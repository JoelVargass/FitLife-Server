using Application.Interfaces.Repositories;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Plans.Commands.Create;

public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand, ErrorOr<Created>>
{
    private readonly IRepository<Plan> _repository;

    public CreatePlanCommandHandler(IRepository<Plan> repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Created>> Handle(CreatePlanCommand command, CancellationToken cancellationToken)
    {
        
        var plan = new Plan
        {
            Name = command.Name,
            Description = command.Description
        };
        
        await _repository.InsertAsync(plan);

        return Result.Created;
    }
}