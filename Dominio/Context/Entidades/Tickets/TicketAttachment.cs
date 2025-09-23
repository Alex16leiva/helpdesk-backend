using Dominio.Core;

namespace Dominio.Context.Entidades.Tickets
{
    public class TicketAttachment : Entity
    {
        public int Id { get; set; } 
        public string TicketId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long Size { get; set; }
    }
}
