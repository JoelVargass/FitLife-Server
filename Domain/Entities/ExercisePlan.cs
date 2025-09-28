namespace Domain.Entities;

public class PlanExercise
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PlanId { get; set; }
    public Plan Plan { get; set; } = null!;

    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;

    public string DayOfWeek { get; set; } = null!; // Lunes, Martes, etc.
    public int? Series { get; set; }
    public int? Repetitions { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteDate { get; set; }
}