using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Services.Authentication;

namespace Infrastructure.Data.Seeders;

public static class Seeder
{
    public static class AdministrationSeeder
    {
        public static async Task SeedAsync(DbContext context)
        {
            // Seeder de usuarios (solo si no existen)
            if (!await context.Set<User>().AnyAsync())
            {
                var users = new List<User>
                {
                    new() { Id = Guid.NewGuid(), Name = "Joel", FirstLastName = "Vargas", SecondLastName = "Pérez", Email = "jovap723@gmail.com", Password = "password" },
                    new() { Id = Guid.NewGuid(), Name = "Celeste", FirstLastName = "Gonzalez", SecondLastName = "Cruz", Email = "23393205@utcancun.edu.mx", Password = "password" },
                    new() { Id = Guid.NewGuid(), Name = "Renata", FirstLastName = "Cortés", SecondLastName = "Mancilla", Email = "23393204@utcancun.edu.mx", Password = "password" },
                    new() { Id = Guid.NewGuid(), Name = "Sergio Joel", FirstLastName = "Trujillo", SecondLastName = "Torres", Email = "sergio@gmail.com", Password = "password" }
                };

                var passwordService = new PasswordService();
                foreach (var user in users)
                {
                    user.Password = passwordService.HashPassword(user, user.Password!);
                }

                await context.Set<User>().AddRangeAsync(users);
                await context.SaveChangesAsync();
            }

            // Seeder de planes por defecto (si no existen)
            if (!await context.Set<Plan>().AnyAsync(p => p.UserId == null))
            {
                // ------------------- PPL Routine -------------------
                var pplPlan = new Plan
                {
                    Name = "PPL Routine",
                    Description = "Push/Pull/Legs split - 3 días de entrenamiento",
                    TypeOfTraining = TypeOfTraining.Strength,
                    PhysicalCondition = PhysicalCondition.Intermediate,
                    UserId = null,
                    ExercisePlans = new List<ExercisePlan>()
                };

                var pushExercises = new List<Exercise>
                {
                    new() { Name = "Bench Press", MuscleType = MuscleType.Chest },
                    new() { Name = "Overhead Press", MuscleType = MuscleType.Shoulders },
                    new() { Name = "Tricep Pushdown", MuscleType = MuscleType.Triceps },
                    new() { Name = "Incline Dumbbell Press", MuscleType = MuscleType.Chest },
                    new() { Name = "Lateral Raises", MuscleType = MuscleType.Shoulders }
                };

                var pullExercises = new List<Exercise>
                {
                    new() { Name = "Pull-ups", MuscleType = MuscleType.Back },
                    new() { Name = "Barbell Row", MuscleType = MuscleType.Back },
                    new() { Name = "Bicep Curl", MuscleType = MuscleType.Biceps },
                    new() { Name = "Face Pull", MuscleType = MuscleType.Back },
                    new() { Name = "Hammer Curl", MuscleType = MuscleType.Biceps }
                };

                var legExercises = new List<Exercise>
                {
                    new() { Name = "Squat", MuscleType = MuscleType.Legs },
                    new() { Name = "Leg Press", MuscleType = MuscleType.Legs },
                    new() { Name = "Romanian Deadlift", MuscleType = MuscleType.Legs },
                    new() { Name = "Calf Raises", MuscleType = MuscleType.Legs },
                    new() { Name = "Lunges", MuscleType = MuscleType.Legs }
                };

                // Asignar días y series/reps
                pplPlan.ExercisePlans.AddRange(pushExercises.Select(e => new ExercisePlan
                {
                    Exercise = e,
                    Plan = pplPlan,
                    DayOfWeek = DayOfWeek.Monday,
                    Series = 4,
                    Repetitions = 8
                }));

                pplPlan.ExercisePlans.AddRange(pullExercises.Select(e => new ExercisePlan
                {
                    Exercise = e,
                    Plan = pplPlan,
                    DayOfWeek = DayOfWeek.Wednesday,
                    Series = 4,
                    Repetitions = 8
                }));

                pplPlan.ExercisePlans.AddRange(legExercises.Select(e => new ExercisePlan
                {
                    Exercise = e,
                    Plan = pplPlan,
                    DayOfWeek = DayOfWeek.Friday,
                    Series = 4,
                    Repetitions = 10
                }));

                // ------------------- Arnold Split -------------------
                var arnoldPlan = new Plan
                {
                    Name = "Arnold Split",
                    Description = "Arnold Schwarzenegger 6-day split",
                    TypeOfTraining = TypeOfTraining.Strength,
                    PhysicalCondition = PhysicalCondition.Advance,
                    UserId = null,
                    ExercisePlans = new List<ExercisePlan>()
                };

                var chestBack = new List<Exercise>
                {
                    new() { Name = "Bench Press", MuscleType = MuscleType.Chest },
                    new() { Name = "Incline Dumbbell Press", MuscleType = MuscleType.Chest },
                    new() { Name = "Pull-ups", MuscleType = MuscleType.Back },
                    new() { Name = "Barbell Row", MuscleType = MuscleType.Back },
                    new() { Name = "Dumbbell Flyes", MuscleType = MuscleType.Chest }
                };

                var shouldersArms = new List<Exercise>
                {
                    new() { Name = "Overhead Press", MuscleType = MuscleType.Shoulders },
                    new() { Name = "Lateral Raises", MuscleType = MuscleType.Shoulders },
                    new() { Name = "Bicep Curl", MuscleType = MuscleType.Biceps },
                    new() { Name = "Tricep Pushdown", MuscleType = MuscleType.Triceps },
                    new() { Name = "Hammer Curl", MuscleType = MuscleType.Biceps }
                };

                var legsAbs = new List<Exercise>
                {
                    new() { Name = "Squat", MuscleType = MuscleType.Legs },
                    new() { Name = "Leg Press", MuscleType = MuscleType.Legs },
                    new() { Name = "Romanian Deadlift", MuscleType = MuscleType.Legs },
                    new() { Name = "Lunges", MuscleType = MuscleType.Legs },
                    new() { Name = "Crunches", MuscleType = MuscleType.Abs }
                };

                // Chest & Back → Lunes y Jueves
                foreach (var day in new[] { DayOfWeek.Monday, DayOfWeek.Thursday })
                {
                    arnoldPlan.ExercisePlans.AddRange(chestBack.Select(e => new ExercisePlan
                    {
                        Exercise = e,
                        Plan = arnoldPlan,
                        DayOfWeek = day,
                        Series = 4,
                        Repetitions = e.MuscleType == MuscleType.Chest || e.MuscleType == MuscleType.Back ? 6 : 10
                    }));
                }

                // Shoulders & Arms → Martes y Viernes
                foreach (var day in new[] { DayOfWeek.Tuesday, DayOfWeek.Friday })
                {
                    arnoldPlan.ExercisePlans.AddRange(shouldersArms.Select(e => new ExercisePlan
                    {
                        Exercise = e,
                        Plan = arnoldPlan,
                        DayOfWeek = day,
                        Series = 3,
                        Repetitions = 10
                    }));
                }

                // Legs & Abs → Miércoles y Sábado
                foreach (var day in new[] { DayOfWeek.Wednesday, DayOfWeek.Saturday })
                {
                    arnoldPlan.ExercisePlans.AddRange(legsAbs.Select(e => new ExercisePlan
                    {
                        Exercise = e,
                        Plan = arnoldPlan,
                        DayOfWeek = day,
                        Series = 4,
                        Repetitions = e.MuscleType == MuscleType.Abs ? 15 : 10
                    }));
                }

                // Insertar planes
                await context.Set<Plan>().AddRangeAsync(pplPlan, arnoldPlan);
                await context.SaveChangesAsync();
            }
        }
    }
}
