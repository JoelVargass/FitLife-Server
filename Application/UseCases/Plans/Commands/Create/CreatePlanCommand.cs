using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Plans.Commands.Create;

public record CreatePlanCommand(
    string Name,
    string? Description,
    TypeOfTraining TypeOfTraining,
    PhysicalCondition PhysicalCondition
    ) : IRequest<ErrorOr<Created>>;