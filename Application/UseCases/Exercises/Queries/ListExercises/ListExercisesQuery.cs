using Application.Common.Results;
using Application.UseCases.Exercises.Common;
using ErrorOr;
using MediatR;

namespace Application.UseCases.Exercises.Queries.ListExercises;

public record ListExercisesQuery(
    Guid UserId,
    int Page = 1, 
    int PageSize = 10, 
    string? Name = null
) : IRequest<ErrorOr<ListResult<ExerciseResult>>>;