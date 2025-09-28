using Application.Interfaces.Repositories;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Exercises.Commands.Update;

public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand, ErrorOr<Updated>>
{
    private readonly IRepository<Exercise> _repository;

    public UpdateExerciseCommandHandler(IRepository<Exercise> repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateExerciseCommand command, CancellationToken cancellationToken)
    {
        /*
        if (!_userService.HasPermission("users.update"))
            return Errors.Role.NotAllowed;
    */
        var exercise = await _repository.GetByIdAsync(command.Id);

        if (exercise is null)
            return Errors.Exercise.NotFound;

        exercise.Name = command.Name;
        exercise.MuscleType = command.MuscleType;
        exercise.Description = command.Description;
        exercise.Duration = command.Duration;
        exercise.UpdateDate = DateTime.UtcNow;

        await _repository.UpdateAsync(exercise);

        return Result.Updated;
    }
    
}