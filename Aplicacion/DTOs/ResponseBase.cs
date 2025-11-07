using Dominio.Core.Extensions;

namespace Aplicacion.DTOs
{
    public abstract class ResponseBase
    {
        public string? Message { get; set; }
        public string? SuccessMessage { get; set; }
        public DateTime? FechaTransaccion { get; set; }


        public bool HasValitationMessage()
        {
            return Message.HasValue();
        }

        public void AppendValidationErrorMessage(string message)
        {
            if (HasValitationMessage())
            {
                Message = $"{Message}, {message}";
                return;
            }
            Message = message;
        }
    }
}
