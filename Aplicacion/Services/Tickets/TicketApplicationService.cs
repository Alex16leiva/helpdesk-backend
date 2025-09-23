using Aplicacion.DTOs.Tickets;
using Dominio.Context.Entidades.Tickets;
using Infraestructura.Context;

namespace Aplicacion.Services.Tickets
{
    public class TicketApplicationService
    {
        private readonly IGenericRepository<IDataContext> _genericRepository;

        public TicketApplicationService(IGenericRepository<IDataContext> genericRepository)
        {
            genericRepository = _genericRepository;
        }

        public async Task<TicketDTO> CreateTicketAsync(TicketRequest ticket)
        {
            throw new NotImplementedException();
        }
    }
}
