namespace WorkoutManager.Models;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime? CreateTime { get; set; }
    public DateTime? UpdateTime { get; set; }
    public string CreateUser { get; set; } = string.Empty;
    public string UpdateUser { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}