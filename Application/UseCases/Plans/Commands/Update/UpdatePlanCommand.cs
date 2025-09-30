using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Plans.Commands.Update;

public record UpdatePlanCommand(
    Guid Id,
    string Name,
    string? Description,
    TypeOfTraining TypeOfTraining,
    PhysicalCondition PhysicalCondition
    ) : IRequest<ErrorOr<Updated>>;