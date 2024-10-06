namespace BACKEND.Errors;

public class CodeErrorException : CodeErrorResponse
{
    public CodeErrorException(int code, string message = "") : base(code, message)
    {
    }
}
