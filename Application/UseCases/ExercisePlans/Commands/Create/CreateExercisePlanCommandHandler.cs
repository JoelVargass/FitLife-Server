using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using ErrorOr;

namespace Application.UseCases.ExercisePlans.Commands;

public class CreateExercisePlanCommandHandler : IRequestHandler<CreateExercisePlanCommand, ErrorOr<Created>>
{
    private readonly IRepository<ExercisePlan> _repository;

    public CreateExercisePlanCommandHandler(IRepository<ExercisePlan> repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Created>> Handle(CreateExercisePlanCommand command, CancellationToken cancellationToken)
    {
        var exercisePlan = new ExercisePlan
        {
            PlanId = command.PlanId,
            ExerciseId = command.ExerciseId,
            DayOfWeek = command.DayOfWeek,
            Series = command.Series,
            Repetitions = command.Repetitions
        };

        await _repository.InsertAsync(exercisePlan);

        return Result.Created;
    }
}