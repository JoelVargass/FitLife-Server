using ErrorOr;
using MediatR;
using Application.Interfaces.Authentication;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Common.Errors;
using Domain.Entities;

namespace Application.UseCases.Users.Commands.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<Updated>>
{
    private readonly IRepository<User> _repository;
    private readonly IUserService _userService;
    private readonly IPasswordService _passwordService;

    public UpdateUserCommandHandler(IRepository<User> repository, IUserService userService,
        IPasswordService passwordService)
    {
        _repository = repository;
        _userService = userService;
        _passwordService = passwordService;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        /*
        if (!_userService.HasPermission("users.update"))
            return Errors.Role.NotAllowed;
    */
        var user = await _repository.GetByIdAsync(command.Id);

        if (user is null)
            return Errors.User.NotFound;

        user.Name = command.Name;
        user.FirstLastName = command.FirstLastName;
        user.SecondLastName = command.SecondLastName;
        user.Email = command.Email;
        user.Password = _passwordService.HashPassword(user, command.Password);
        //user.RoleId = command.RoleId;
        user.UpdateDate = DateTime.UtcNow;

        await _repository.UpdateAsync(user);

        return Result.Updated;
    }
}