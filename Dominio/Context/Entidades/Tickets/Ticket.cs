using Dominio.Core;

namespace Dominio.Context.Entidades.Tickets
{
    public enum TicketStatus { Open = 0, InProgress = 1, Resolved = 2, Closed = 3 }

    public class Ticket : Entity
    {
        public string TicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; } // 1-low,2-medium,3-high
        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public string CreatedBy { get; set; }
        public string AssignedToUserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateClosed { get; set; }
        public virtual ICollection<TicketComment> Comments { get; set; } = [];
        public virtual ICollection<TicketAttachment> Attachments { get; set; } = [];
    }
}
