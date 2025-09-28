using ErrorOr;
using MediatR;

namespace Application.UseCases.Plans.Commands.Update;

public record UpdatePlanCommand(
    Guid Id,
    string Name,
    string? Description
    ) : IRequest<ErrorOr<Updated>>;