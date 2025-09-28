using ErrorOr;
using MediatR;

namespace Application.UseCases.Plans.Commands.Create;

public record CreatePlanCommand(
    string Name,
    string? Description
    ) : IRequest<ErrorOr<Created>>;