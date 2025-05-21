using Application.Domain.Response;

namespace Application.External.Response;

public class ExternalResponse : BaseResponse
{
    // TRUE
    public static ExternalResponse Ok()
    {
        return new ExternalResponse { Success = true, StatusCode = 200 };
    }
    public static ExternalResponse Created()
    {
        return new ExternalResponse { Success = true, StatusCode = 201 };
    }

    // FALSE
    public static ExternalResponse BadRequest(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 400, Message = message };
    }
    public static ExternalResponse Unauthorized(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 401, Message = message };
    }
    public static ExternalResponse Forbidden(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 403, Message = message };
    }
    public static ExternalResponse NotFound(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 404, Message = message };
    }
    public static ExternalResponse AlreadyExists(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 409, Message = message };
    }
    public static ExternalResponse Error(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 500, Message = message };
    }
    public static ExternalResponse BadGateway(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 502, Message = message };
    }
    public static ExternalResponse ServiceUnavailable(string? message)
    {
        return new ExternalResponse { Success = false, StatusCode = 504, Message = message };
    }
}
