using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Users.Commands.UpdateProfile;

public record UpdateProfileCommand(
    Guid Id,
    string Name,
    string FirstLastName,
    string? SecondLastName,
    Genre? Genre,
    decimal? Weight,
    decimal? Height
    ) : IRequest<ErrorOr<Updated>>;