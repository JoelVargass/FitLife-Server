namespace Application.UseCases.ExercisePlans.Common;

public record ExercisePlanResult(
    Guid Id,
    Guid PlanId,
    Guid ExerciseId,
    DayOfWeek DayOfWeek,
    int? Series,
    int? Repetitions
    );