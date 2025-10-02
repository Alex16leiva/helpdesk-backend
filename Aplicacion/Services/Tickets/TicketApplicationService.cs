using Aplicacion.DTOs;
using Aplicacion.DTOs.Tickets;
using Aplicacion.Helpers;
using Dominio.Context;
using Dominio.Context.Entidades.Tickets;
using Dominio.Core;
using Dominio.Core.Extensions;
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

        public TicketDTO CreateTicket(TicketRequest request)
        {
            var nuevoTicket = new Ticket
            {
                TicketId = IdentityFactory.CreateIdentity().NextCorrelativeIdentity(Correlativo.Ticket),
                AsignadoAUsuario = request.Ticket.AsignadoAUsuario,
                CreadoPor = request.RequestUserInfo.UsuarioId,
                Descripcion = request.Ticket.Descripcion,
                Estado = Status.Open,
                Prioridad = 1,
                Titulo = request.Ticket.Titulo,
                TicketComment = [],
                TicketAttachment = [],
            };

            _genericRepository.Add(nuevoTicket);
            TransactionInfo transactionInfo = request.RequestUserInfo.CrearTransactionInfo("CrearTicket");
            _genericRepository.UnitOfWork.Commit(transactionInfo);
            return request.Ticket;
        }
        public async Task<TicketDTO> UpdateTicketAsync(TicketRequest request)
        {
            Ticket ticket = await _genericRepository.GetSingleAsync<Ticket>(r => r.TicketId == request.Ticket.TicketId);
            if (ticket.IsNull())
            {
                return new TicketDTO
                {
                    Message = $"El ticket {request.Ticket.TicketId} no existe"
                };
            }
            if(ticket.IsClosed())
            {
                return new TicketDTO
                {
                    Message = $"El ticket {request.Ticket.TicketId} ya esta cerrado"
                };
            }
            ticket.Prioridad = (int)request.Ticket.Prioridad;
            ticket.Estado = request.Ticket.Estado;
            ticket.AsignarUsuario(request.Ticket.AsignadoAUsuario);
            TransactionInfo transactionInfo = request.RequestUserInfo.CrearTransactionInfo("EditarTicket");
            _genericRepository.UnitOfWork.Commit(transactionInfo);
            return new TicketDTO();
        }
        public async Task<TicketDTO> GetTicketAsync(TicketRequest request)
        {
            List<string> includes = new List<string> { "TicketComment" };
            Ticket ticket = await _genericRepository.GetSingleAsync<Ticket>(r => r.TicketId == request.Ticket.TicketId, includes);
            if (ticket.IsNull())
            {
                return new TicketDTO
                {
                    Message = $"El ticket {request.Ticket.TicketId} no existe"
                };
            }

            return MapTicketDto(ticket);
        }
        public SearchResult<TicketDTO> GetAllTickets(TicketRequest request)
        {
            request.QueryInfo.Includes = new List<string> { "TicketComment" };
            var dynamicFilter = DynamicFilterFactory.CreateDynamicFilter(request.QueryInfo);
            PagedCollection tickets = _genericRepository.GetPagedAndFiltered<Ticket>(dynamicFilter);

            return new SearchResult<TicketDTO>
            {
                PageCount = tickets.PageCount,
                ItemCount = tickets.ItemCount,
                TotalItems = tickets.TotalItems,
                PageIndex = tickets.PageIndex,
                Items = (from ticket in tickets.Items as IEnumerable<Ticket> select MapTicketDto(ticket)).ToList(),
            };
        }
        private static TicketDTO MapTicketDto(Ticket ticket)
        {
            return new TicketDTO
            {
                TicketId = ticket.TicketId,
                Titulo = ticket.Titulo,
                Descripcion = ticket.Descripcion,
                Prioridad = ticket.Prioridad,
                Estado = ticket.Estado,
                CreadoPor = ticket.CreadoPor,
                AsignadoAUsuario = ticket.AsignadoAUsuario,
                FechaCreado = ticket.FechaCreado,
                FechaTransaccion = ticket.FechaTransaccion,
                Comentarios = MapComentarios(ticket.TicketComment),
            };
        }
        private static List<TicketCommentDTO> MapComentarios(ICollection<TicketComment> comentarios)
        {
            return comentarios.Select(x => new TicketCommentDTO
            {
                Id = x.Id,
                Comentario = x.Comentario,
                TicketId = x.TicketId,
                FechaTransaccion = x.FechaTransaccion
            }).ToList();
        }
        public TicketCommentDTO CreateComment(TicketCommentRequest request)
        {
            string ticketId = request.TicketComment.TicketId;
            Ticket ticket = _genericRepository.GetSingle<Ticket>(r => r.TicketId == ticketId);
            if (ticket.IsNull())
            {
                return new TicketCommentDTO
                {
                    Message = $"El ticket {ticketId} no existe"
                };
            }
            if (ticket.IsClosed())
            {
                return new TicketCommentDTO
                {
                    Message = $"El ticket {ticketId} ya esta cerrado"
                };
            }

            TicketComment nuevoComentario = new()
            {
                TicketId = ticket.TicketId,
                Comentario = request.TicketComment.Comentario,
            };

            _genericRepository.Add(nuevoComentario);
            TransactionInfo transactionInfo = request.RequestUserInfo.CrearTransactionInfo("CrearComentario");
            _genericRepository.UnitOfWork.Commit(transactionInfo);

            return new TicketCommentDTO();
        }
    }
}
