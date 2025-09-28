using Application.UseCases.Users.Commands.Delete;
using FluentValidation;

namespace Application.UseCases.Plans.Commands.Delete;

public class DeletePlanCommandValidator : AbstractValidator<DeletePlanCommand>
{
    public DeletePlanCommandValidator()
    {
        
    }
}