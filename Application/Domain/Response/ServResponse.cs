namespace Application.Domain.Response;


public class ServResponse : BaseResponse
{
    // TRUE
    public static ServResponse Ok()
    {
        return new ServResponse { Success = true, StatusCode = 200 };
    }
    public static ServResponse Created()
    {
        return new ServResponse { Success = true, StatusCode = 201 };
    }

    // FALSE
    public static ServResponse BadRequest(string? message)
    {
        return new ServResponse { Success = false, StatusCode = 400, Message = message };
    }
    public static ServResponse Unauthorized(string? message)
    {
        return new ServResponse { Success = false, StatusCode = 401, Message = message };
    }
    public static ServResponse Forbidden(string? message)
    {
        return new ServResponse { Success = false, StatusCode = 403, Message = message };
    }
    public static ServResponse NotFound(string? message)
    {
        return new ServResponse { Success = false, StatusCode = 404, Message = message };
    }
    public static ServResponse AlreadyExists(string? message)
    {
        return new ServResponse { Success = false, StatusCode = 409, Message = message };
    }
    public static ServResponse Error(string? message)
    {
        return new ServResponse { Success = false, StatusCode = 500, Message = message };
    }
    public static ServResponse BadGateway(string? message)
    {
        return new ServResponse { Success = false, StatusCode = 502, Message = message };
    }
    public static ServResponse ServiceUnavailable(string? message)
    {
        return new ServResponse { Success = false, StatusCode = 504, Message = message };
    }

}
public class ServResponse<T> : BaseResponse<T> where T : class
{
    // TRUE
    public static ServResponse<T> Ok(T? content)
    {
        return new ServResponse<T> { Success = true, StatusCode = 200, Content = content };
    }

    // FALSE
    public static ServResponse<T> BadRequest(string? message, T? content)
    {
        return new ServResponse<T> { Success = false, StatusCode = 400, Message = message, Content = content };
    }
    public static ServResponse<T> NotFound(string? message, T? content)
    {
        return new ServResponse<T> { Success = false, StatusCode = 404, Message = message, Content = content };
    }
    public static ServResponse<T> AlreadyExists(string? message, T? content)
    {
        return new ServResponse<T> { Success = false, StatusCode = 409, Message = message, Content = content };
    }
    public static ServResponse<T> Error(string? message, T? content)
    {
        return new ServResponse<T> { Success = false, StatusCode = 500, Message = message, Content = content };
    }
}