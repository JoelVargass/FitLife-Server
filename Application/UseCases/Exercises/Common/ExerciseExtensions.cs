using Domain.Entities;

namespace Application.UseCases.Exercises.Common;

public static class ExerciseExtensions
{
    public static ExerciseResult? ToResult(this Exercise exercise)
    {
        return new ExerciseResult(
            exercise.Id,
            exercise.Name,
            exercise.MuscleType,
            exercise.Description,
            exercise.Duration
            );
    }
}