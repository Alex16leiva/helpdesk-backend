namespace Aplicacion.DTOs.Tickets
{
    public class TicketRequest : RequestBase
    {
        public TicketDTO? Ticket { get; set; }
    }

    public class TicketCommentRequest : RequestBase
    {
        public TicketCommentDTO? TicketComment { get; set; }
    }
}
