namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string FirstLastName { get; set; }
    public string? SecondLastName { get; set; }
    public string? Password { get; set; }
    public Genre? Genre { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public string? RecoveryToken { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteDate { get; set; }
}

public enum Genre
{
    Feminine,
    Masculine,
    Other
}