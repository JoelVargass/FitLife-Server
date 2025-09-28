using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.UseCases.Exercises.Commands.Delete;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Plans.Commands.Delete;

public class DeletePlanCommandHandler : IRequestHandler<DeletePlanCommand, ErrorOr<Unit>>
{
    private readonly IRepository<Plan> _repository;
    private readonly IUserService _userService;

    public DeletePlanCommandHandler(IRepository<Plan> repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<ErrorOr<Unit>> Handle(DeletePlanCommand command, CancellationToken cancellationToken)
    {
        /*
        if (!_userService.HasPermission(PermissionsConstants.Users.Delete))
            return Errors.Role.NotAllowed;
    */
        var plan = await _repository.GetByIdAsync(command.Id);

        if (plan is null)
            return Errors.Plan.NotFound;

        plan.IsDeleted = true;
        plan.DeleteDate = DateTime.UtcNow;

        await _repository.UpdateAsync(plan);

        return Unit.Value;
    }
}