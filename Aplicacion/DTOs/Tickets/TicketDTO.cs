using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs.Tickets
{
    public class TicketDTO : ResponseBase
    {
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public int? Prioridad { get; set; }
        public string? Estado { get; set; }

    }
}
