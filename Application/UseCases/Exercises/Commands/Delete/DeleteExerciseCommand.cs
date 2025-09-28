using ErrorOr;
using MediatR;

namespace Application.UseCases.Exercises.Commands.Delete;

public record DeleteExerciseCommand(Guid Id) : IRequest<ErrorOr<Unit>>;