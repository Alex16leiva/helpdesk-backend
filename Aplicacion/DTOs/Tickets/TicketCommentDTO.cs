namespace Aplicacion.DTOs.Tickets
{
    public class TicketCommentDTO : ResponseBase
    {
        public int? Id { get; set; }
        public string? TicketId { get; set; }
        public string? ModificadoPor { get; set; }
        public string? Comentario { get; set; }
    }
}
