namespace Application.Domain.Response;

public class BaseResponse
{
    public bool Success { get; protected set; }
    public int StatusCode { get; protected set; }
    public string? Message { get; protected set; }
}
public class BaseResponse<T> : BaseResponse where T : class
{
    public T? Content { get; protected set; }
}