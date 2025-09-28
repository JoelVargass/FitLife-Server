using Application.Interfaces.Repositories;
using Application.UseCases.Exercises.Commands.Update;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Plans.Commands.Update;

public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand, ErrorOr<Updated>>
{
    private readonly IRepository<Plan> _repository;

    public UpdatePlanCommandHandler(IRepository<Plan> repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdatePlanCommand command, CancellationToken cancellationToken)
    {
        /*
        if (!_userService.HasPermission("users.update"))
            return Errors.Role.NotAllowed;
    */
        var plan = await _repository.GetByIdAsync(command.Id);

        if (plan is null)
            return Errors.Plan.NotFound;

        plan.Name = command.Name;
        plan.Description = command.Description;
        plan.UpdateDate = DateTime.UtcNow;

        await _repository.UpdateAsync(plan);

        return Result.Updated;
    }
    
}