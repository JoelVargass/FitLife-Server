using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.UseCases.Users.Commands.Create;
using Application.UseCases.Users.Commands.Delete;
using Application.UseCases.Users.Commands.Update;
using Application.UseCases.Users.Commands.UpdateProfile;
using Application.UseCases.Users.Common;
using Application.UseCases.Users.Queries.ListUsers;
using Domain.Common.Errors;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using WebApi.Common.Controllers;

//using Domain.Common.Constants;

namespace WebApi.Controllers;

public class UsersController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IRepository<User> _repository;
    private readonly IUserRepository _userRepository;

    public UsersController(IMediator mediator, IUserService userService, IMapper mapper,
        IRepository<User> repository, IUserRepository userRepository)
    {
        _mediator = mediator;
        _userService = userService;
        _mapper = mapper;
        _repository = repository;
        _userRepository = userRepository;
    }

    public record ListUsersRequest(int Page = 1, int PageSize = 10, string? Search = null, Guid? RoleId = null);

    public record UserCreateRequest(
        string Name,
        string FirstLastName,
        string SecondLastName,
        string Email,
        string Password
        //Guid RoleId,
        );

    public record UserUpdateProfileRequest(
        Guid Id,
        string Name,
        string FirstLastName,
        string? SecondLastName);

    public record UserUpdateRequest(
        Guid Id,
        string Name,
        string FirstLastName,
        string SecondLastName,
        string Email,
        string Password
        //Guid RoleId
        );

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] ListUsersRequest request)
    {
        var query = new ListUsersQuery(request.Page, request.PageSize, request.Search );
        var result = await _mediator.Send(query);

        return result.Match(Ok, Problem);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _userRepository.IncludeRoleAsync(id);

        return user is null ? Problem(Errors.User.NotFound) : Ok(user.ToResult());
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserCreateRequest request)
    {
        var command = new CreateUserCommand(request.Name, request.FirstLastName, request.SecondLastName,
            request.Email, request.Password);
        var result = await _mediator.Send(command);

        return result.Match(_ => Ok(), Problem);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UserUpdateRequest request)
    {
        var command = new UpdateUserCommand(request.Id, request.Name, request.FirstLastName, request.SecondLastName,
            request.Email, request.Password);
        var result = await _mediator.Send(command);

        return result.Match(
            _ => Ok(),
            Problem);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfileData(UserUpdateProfileRequest request)
    {
        var userId = _userService.GetUserId();

        if (userId != request.Id)
            return Problem(Errors.Authentication.NotAuthorized);

        var command = new UpdateProfileCommand(request.Id, request.Name, request.FirstLastName, request.SecondLastName);

        var result = await _mediator.Send(command);

        return result.Match(
            _ => Ok(),
            Problem);
    }

    /*
    [HttpPatch("profile-image")]
    public async Task<IActionResult> UploadProfileImage(IFormFile image, [FromForm] Guid userId)
    {
        var user = await _repository.GetByIdAsync(userId);

        if (user is null)
            return Problem(Errors.User.NotFound);

        if (user.Id != _userService.GetUserId())
            return Problem(Errors.Authentication.NotAuthorized);

        var result = await _imageService.UploadAsync(
            image.FileName,
            image.OpenReadStream(),
            userId.ToString());

        if (result.IsError)
            return Problem(result.Errors);

        var filePath = result.Value;

        user.Picture = filePath;

        await _repository.UpdateAsync(user);

        return Ok(filePath);
    }
    */

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var command = new DeleteUserCommand(id);
        var result = await _mediator.Send(command);

        return result.Match(
            _ => Ok(),
            Problem);
    }
}