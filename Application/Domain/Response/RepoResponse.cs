namespace Application.Domain.Response;


public class RepoResponse : BaseResponse
{
    // TRUE
    public static RepoResponse Ok()
    {
        return new RepoResponse { Success = true, StatusCode = 200 };
    }
    public static RepoResponse Created()
    {
        return new RepoResponse { Success = true, StatusCode = 201 };
    }

    // FALSE
    public static RepoResponse BadRequest(string? message)
    {
        return new RepoResponse { Success = false, StatusCode = 400, Message = message };
    }
    public static RepoResponse Unauthorized(string? message)
    {
        return new RepoResponse { Success = false, StatusCode = 401, Message = message };
    }
    public static RepoResponse Forbidden(string? message)
    {
        return new RepoResponse { Success = false, StatusCode = 403, Message = message };
    }
    public static RepoResponse NotFound(string? message)
    {
        return new RepoResponse { Success = false, StatusCode = 404, Message = message };
    }
    public static RepoResponse AlreadyExists(string? message)
    {
        return new RepoResponse { Success = false, StatusCode = 409, Message = message };
    }
    public static RepoResponse Error(string? message)
    {
        return new RepoResponse { Success = false, StatusCode = 500, Message = message };
    }
    public static RepoResponse BadGateway(string? message)
    {
        return new RepoResponse { Success = false, StatusCode = 502, Message = message };
    }
    public static RepoResponse ServiceUnavailable(string? message)
    {
        return new RepoResponse { Success = false, StatusCode = 504, Message = message };
    }

}
public class RepoResponse<T> : BaseResponse<T> where T : class
{
    // TRUE
    public static RepoResponse<T> Ok(T? content)
    {
        return new RepoResponse<T> { Success = true, StatusCode = 200, Content = content };
    }

    // FALSE
    public static RepoResponse<T> BadRequest(string? message, T? content)
    {
        return new RepoResponse<T> { Success = false, StatusCode = 400, Message = message, Content = content };
    }
    public static RepoResponse<T> NotFound(string? message, T? content)
    {
        return new RepoResponse<T> { Success = false, StatusCode = 404, Message = message, Content = content };
    }
    public static RepoResponse<T> AlreadyExists(string? message, T? content)
    {
        return new RepoResponse<T> { Success = false, StatusCode = 409, Message = message, Content = content };
    }
    public static RepoResponse<T> Error(string? message, T? content)
    {
        return new RepoResponse<T> { Success = false, StatusCode = 500, Message = message, Content = content };
    }
}