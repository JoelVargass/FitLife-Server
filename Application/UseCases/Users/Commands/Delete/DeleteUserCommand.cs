using ErrorOr;
using MediatR;

namespace Application.UseCases.Users.Commands.Delete;

public record DeleteUserCommand(Guid Id) : IRequest<ErrorOr<Unit>>;