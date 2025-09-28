using ErrorOr;
using MediatR;

namespace Application.UseCases.Plans.Commands.Delete;

public record DeletePlanCommand(Guid Id) : IRequest<ErrorOr<Unit>>;