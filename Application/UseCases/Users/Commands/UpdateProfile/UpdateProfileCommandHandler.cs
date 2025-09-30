using ErrorOr;
using MediatR;
using Application.Interfaces.Repositories;
using Domain.Common.Errors;
using Domain.Entities;

namespace Application.UseCases.Users.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ErrorOr<Updated>>
{
    private readonly IRepository<User> _userRepository;

    public UpdateProfileCommandHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
            return Errors.User.NotFound;
        
        user.Name = request.Name;
        user.FirstLastName = request.FirstLastName;
        user.SecondLastName = request.SecondLastName;
        user.Genre = request.Genre;
        user.Weight = request.Weight;
        user.Height = request.Height;
        
        await _userRepository.UpdateAsync(user);

        return Result.Updated;
    }
}