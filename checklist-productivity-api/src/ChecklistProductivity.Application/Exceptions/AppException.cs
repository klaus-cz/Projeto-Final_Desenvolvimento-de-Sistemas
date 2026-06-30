namespace ChecklistProductivity.Application.Exceptions;

public abstract class AppException : Exception
{
    public int StatusCode { get; }
    public string ErrorCode { get; }

    protected AppException(string message, int statusCode, string errorCode) : base(message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }
}

public class NotFoundException : AppException
{
    public NotFoundException(string message) : base(message, 404, "not_found") { }
}

public class ConflictException : AppException
{
    public ConflictException(string message) : base(message, 409, "conflict") { }
}

public class UnauthorizedAppException : AppException
{
    public UnauthorizedAppException(string message) : base(message, 401, "unauthorized") { }
}

public class ForbiddenAppException : AppException
{
    public ForbiddenAppException(string message) : base(message, 403, "forbidden") { }
}

public class ValidationAppException : AppException
{
    public ValidationAppException(string message) : base(message, 400, "validation_error") { }
}
