
using Dominio.Core.Extensions;

namespace Aplicacion.DTOs.Tickets
{
    public class TicketRequest : RequestBase
    {
        public TicketDTO? Ticket { get; set; }

        internal string ObtenerUsuarioQueCreoTicket()
        {
            return Ticket.ObtenerUsuarioQueCreoTicket().HasValue() ? Ticket.ObtenerUsuarioQueCreoTicket() :  RequestUserInfo.UsuarioId;
        }
    }

    public class TicketCommentRequest : RequestBase
    {
        public TicketCommentDTO? TicketComment { get; set; }
    }
}
