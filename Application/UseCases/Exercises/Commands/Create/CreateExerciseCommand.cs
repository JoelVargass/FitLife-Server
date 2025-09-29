using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Exercises.Commands.Create;

public record CreateExerciseCommand(
    string Name,
    MuscleType MuscleType,
    string? Description,
    string? Duration,
    Guid UserId
    ) : IRequest<ErrorOr<Created>>;