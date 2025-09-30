using ErrorOr;
using MediatR;

namespace Application.UseCases.ExercisePlans.Commands;

public record CreateExercisePlanCommand(
    Guid PlanId,
    Guid ExerciseId,
    DayOfWeek DayOfWeek,
    int Series,
    int Repetitions
    ) : IRequest<ErrorOr<Created>>;