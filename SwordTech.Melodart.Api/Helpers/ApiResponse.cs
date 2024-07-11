namespace SwordTech.Melodart.Api.Helpers;

public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string InternalMessage { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
}


public class ApiResponse<T> : ApiResponse
{
    public T Data { get; set; }
}
