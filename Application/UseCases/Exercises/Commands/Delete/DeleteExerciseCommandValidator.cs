using Application.UseCases.Users.Commands.Delete;
using FluentValidation;

namespace Application.UseCases.Exercises.Commands.Delete;

public class DeleteExerciseCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteExerciseCommandValidator()
    {
        
    }
}