using Dominio.Core;
using Dominio.Core.Extensions;

namespace Dominio.Context.Entidades.Tickets
{
    public class Ticket : Entity
    {
        public required string TicketId { get; set; }
        public required string Titulo { get; set; }
        public required string Descripcion { get; set; }
        public required int Prioridad { get; set; } // 1-low,2-medium,3-high
        public required string Estado { get; set; }
        public required string CreadoPor { get; set; }
        public required string AsignadoAUsuario { get; set; }
        public DateTime FechaCreado { get; set; } = DateTime.UtcNow;
        public virtual ICollection<TicketComment> TicketComment { get; set; } = [];
        public virtual ICollection<TicketAttachment> TicketAttachment { get; set; } = [];

        public void AsignarUsuario(string asignadoAUsuario)
        {
            if (AsignadoAUsuario != asignadoAUsuario)
            {
                AsignadoAUsuario = asignadoAUsuario;
                if (!IsClosed())
                {
                    if (AsignadoAUsuario.IsEmpty())
                    {
                        Estado = Status.Open;
                    }
                    else 
                    {
                        Estado = Status.InProcess;
                    }
                }
            }
        }

        public bool IsClosed()
        {
            return Estado == Status.Close;
        }
    }
}
