using ErrorOr;
using MediatR;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Common.Errors;
using Domain.Entities;

namespace Application.UseCases.Users.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<Unit>>
{
    private readonly IRepository<User> _repository;
    private readonly IUserService _userService;

    public DeleteUserCommandHandler(IRepository<User> repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        /*
        if (!_userService.HasPermission(PermissionsConstants.Users.Delete))
            return Errors.Role.NotAllowed;
    */
        var user = await _repository.GetByIdAsync(command.Id);

        if (user is null)
            return Errors.User.NotFound;

        user.IsDeleted = true;
        user.DeleteDate = DateTime.UtcNow;

        await _repository.UpdateAsync(user);

        return Unit.Value;
    }
}