﻿using Domain.Models;

namespace Domain.Models;

public class AddProjectFormData
{
    public int Id { get; set; }
    public string? Image { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? Budget { get; set; }


    public string ClientId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public int StatusId { get; set; }

}
