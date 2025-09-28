using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.UseCases.Users.Commands.Delete;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Exercises.Commands.Delete;

public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand, ErrorOr<Unit>>
{
    private readonly IRepository<Exercise> _repository;
    private readonly IUserService _userService;

    public DeleteExerciseCommandHandler(IRepository<Exercise> repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteExerciseCommand command, CancellationToken cancellationToken)
    {
        /*
        if (!_userService.HasPermission(PermissionsConstants.Users.Delete))
            return Errors.Role.NotAllowed;
    */
        var exercise = await _repository.GetByIdAsync(command.Id);

        if (exercise is null)
            return Errors.Exercise.NotFound;

        exercise.IsDeleted = true;
        exercise.DeleteDate = DateTime.UtcNow;

        await _repository.UpdateAsync(exercise);

        return Unit.Value;
    }
}