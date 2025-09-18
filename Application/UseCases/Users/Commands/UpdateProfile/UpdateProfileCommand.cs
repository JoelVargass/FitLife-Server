using ErrorOr;
using MediatR;

namespace Application.UseCases.Users.Commands.UpdateProfile;

public record UpdateProfileCommand(
    Guid Id,
    string Name,
    string FirstLastName,
    string? SecondLastName) : IRequest<ErrorOr<Updated>>;