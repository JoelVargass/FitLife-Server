using Application.UseCases.Users.Queries.ListUsers;
using FluentValidation;

namespace Application.UseCases.Exercises.Queries.ListExercises;

public class ListExercisesQueryValidator : AbstractValidator<ListUsersQuery>
{
    public ListExercisesQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("La página debe ser mayor o igual a 1.");

    }
}