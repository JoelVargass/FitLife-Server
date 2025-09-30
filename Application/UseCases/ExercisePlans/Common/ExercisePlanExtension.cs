using Domain.Entities;

namespace Application.UseCases.ExercisePlans.Common;

public static class ExercisePlanExtension
{
    public static ExercisePlanResult ToResult(this ExercisePlan exercisePlan)
    {
        return new ExercisePlanResult(
            exercisePlan.Id,
            exercisePlan.PlanId,
            exercisePlan.ExerciseId,
            exercisePlan.DayOfWeek,
            exercisePlan.Series,
            exercisePlan.Repetitions
        );
    }
}