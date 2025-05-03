namespace Domain.Models;

public class Project
{
    public string? Image { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? Budget { get; set; }
    public  Client Client { get; set; } = null!;
    public User User { get; set; } = null!;
    public Status Status { get; set; } = null!;
}