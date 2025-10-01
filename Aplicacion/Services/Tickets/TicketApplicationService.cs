using Aplicacion.DTOs.Tickets;
using Aplicacion.Helpers;
using Dominio.Context;
using Dominio.Context.Entidades.Tickets;
using Dominio.Core;
using Infraestructura.Context;
using Infraestructura.Core.Identity;

namespace Aplicacion.Services.Tickets
{
    public class TicketApplicationService
    {
        private readonly IGenericRepository<IDataContext> _genericRepository;

        public TicketApplicationService(IGenericRepository<IDataContext> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<TicketDTO> CreateTicketAsync(TicketRequest request)
        {
            var nuevoTicket = new Ticket
            {
                TicketId = IdentityFactory.CreateIdentity().NextCorrelativeIdentity(Correlativo.Ticket),
                AsignadoAUsuario = request.Ticket.AsignadoAUsuario,
                CreadoPor = request.RequestUserInfo.UsuarioId,
                Descripcion = request.Ticket.Descripcion,
                Estado = Estado.Open,
                Prioridad = 1,
                Titulo = request.Ticket.Titulo,
                Comentarios = [],
                Adjuntos = [],
            };

            _genericRepository.Add(nuevoTicket);
            TransactionInfo transactionInfo = request.RequestUserInfo.CrearTransactionInfo("EditarUsuario");
            _genericRepository.UnitOfWork.Commit(transactionInfo);
            return request.Ticket;
        }
    }
}
