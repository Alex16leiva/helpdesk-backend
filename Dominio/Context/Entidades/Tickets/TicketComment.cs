using Dominio.Core;

namespace Dominio.Context.Entidades.Tickets
{
    public class TicketComment : Entity
    {
        public int Id { get; set; }
        public string TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public string CreadoPor { get; set; }
        public string Mensaje { get; set; }
    }
}
