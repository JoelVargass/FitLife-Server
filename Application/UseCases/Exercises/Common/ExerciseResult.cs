namespace Application.UseCases.Exercises.Common;

public record ExerciseResult(
    Guid Id,
    string Name,
    string? MuscleType,
    string? Description,
    string? Duration
    );