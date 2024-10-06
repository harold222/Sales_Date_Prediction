namespace BACKEND.Errors;

public class CodeErrorResponse
{
    public string mensaje { get; set; }

    public CodeErrorResponse(int code, string message = "")
    {
        this.mensaje = message;
        this.mensaje = string.IsNullOrEmpty(message) ?
                            GetDefaultError(code) :
                            message;
    }

    private string GetDefaultError(int code)
    {
        switch (code)
        {
            case 400:
                return "La solicitud posee errores";
            case 404:
                return "No se encontro el recurso solicitado";
            case 500:
                return "Se ha generado un error";
            default:
                return string.Empty;
        }
    }
}