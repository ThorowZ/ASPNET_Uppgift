namespace Data.Models;

public class RepositoryResult<TResult>
{
    public bool Success { get; set; }

    public int StatusCode { get; set; }

    public string? ErrorMessage { get; set; }
    public TResult? Result { get; set; }

}

