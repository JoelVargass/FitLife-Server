using ErrorOr;
using MediatR;
using Application.Interfaces.Authentication;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Common.Errors;
using Domain.Entities;
using Application.UseCases.Users.Commands.Create;

namespace Application.UseCases.Users.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<Created>>
{
    private readonly IRepository<User> _repository;
    private readonly IUserService _userService;
    private readonly IPasswordService _passwordService;

    public CreateUserCommandHandler(IRepository<User> repository, IUserService userService,
        IPasswordService passwordService)
    {
        _repository = repository;
        _userService = userService;
        _passwordService = passwordService;
    }

    public async Task<ErrorOr<Created>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        /*
        if (!_userService.HasPermission(PermissionsConstants.Users.Create))
            return Errors.Role.NotAllowed;
    */
        var user = new User
        {
            Name = command.Name,
            FirstLastName = command.FirstLastName,
            SecondLastName = command.SecondLastName,
            Email = command.Email,
            Password = command.Password,
            //RoleId = command.RoleId,
        };
        
        user.Password = _passwordService.HashPassword(user, user.Password);

        await _repository.InsertAsync(user);

        return Result.Created;
    }
}