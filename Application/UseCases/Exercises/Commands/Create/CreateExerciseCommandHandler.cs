using Application.Interfaces.Repositories;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Exercises.Commands.Create;

public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, ErrorOr<Created>>
{
    private readonly IRepository<Exercise> _repository;

    public CreateExerciseCommandHandler(IRepository<Exercise> repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Created>> Handle(CreateExerciseCommand command, CancellationToken cancellationToken)
    {
        
        var exercise = new Exercise
        {
            Name = command.Name,
            MuscleType = command.MuscleType,
            Description = command.Description,
            Duration = command.Duration
        };
        
        await _repository.InsertAsync(exercise);

        return Result.Created;
    }
}