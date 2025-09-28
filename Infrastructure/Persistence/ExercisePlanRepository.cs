using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Persistence;

public class ExercisePlanRepository : GenericRepository<ExercisePlan>, IExercisePlanRepository
{
    public ExercisePlanRepository(AppDbContext context) : base(context)
    {
    }
}