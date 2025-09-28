using ErrorOr;
using MediatR;

namespace Application.UseCases.Exercises.Commands.Update;

public record UpdateExerciseCommand(
    Guid Id,
    string Name,
    string? MuscleType,
    string? Description,
    string? Duration
    ) : IRequest<ErrorOr<Updated>>;