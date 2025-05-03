namespace Business.Dtos;

public class ServiceResult
{
    public bool Success { get; set; }

    public int StatusCode { get; set; }

    public string? ErrorMessage { get; set; }
}
