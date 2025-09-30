using System.Linq.Expressions;
using ErrorOr;
using Application.Common.Results;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IExerciseRepository : IRepository<Exercise>
{
}