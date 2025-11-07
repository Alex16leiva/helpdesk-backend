using Aplicacion.DTOs;
using Aplicacion.DTOs.Tickets;
using Aplicacion.Helpers;
using Dominio.Context;
using Dominio.Context.Entidades.Tickets;
using Dominio.Core;
using Dominio.Core.Extensions;
using Infraestructura.Context;
using Infraestructura.Core.Identity;
using System.Collections.Generic;

namespace Aplicacion.Services.Tickets
{
    public class TicketApplicationService : IDisposable
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
                Prioridad = (int)request.Ticket.Prioridad,
                Titulo = request.Ticket.Titulo,
                TicketComment = [],
                TicketAttachment = [],
            };

            _genericRepository.Add(nuevoTicket);
            CommitTransaction(request.RequestUserInfo, "CrearTicket");
            return request.Ticket;
        }
        public List<TicketsInfoDTO> GetDashBoardInfo()
        {
            return [.. GetActiveTickets(), .. GetSolveTickets(), .. GetOlderTicket()];
        }
        private List<TicketsInfoDTO> GetOlderTicket()
        {
            var query = @"SELECT TOP 1 'OlderTicket' As Estado, TicketId As Valor  
                        FROM Ticket 
                        where Estado IN('Open','InProcess')
                        ORDER BY FechaCreado ";

            var parameters = new Microsoft.Data.SqlClient.SqlParameter[0];

            var result = _genericRepository.ExecuteQuery<TicketsInfoDTO>(parameters, query);

            return result.ToList();
        }
        private List<TicketsInfoDTO> GetSolveTickets()
        {
            var currentDate = DateTime.Now; 

            

            var query = @"SELECT Estado, CAST(COUNT(*) AS VARCHAR(20)) AS Valor
                        FROM Ticket 
                        WHERE Estado NOT IN ('Open', 'InProcess')
                        AND FechaTransaccion >= @StartDate 
                        AND FechaTransaccion < @EndDate 
                        GROUP BY Estado";

            var primerDiaDelMes = currentDate.FirstDateOfMonth();
            var ultimoDiaDelMes = currentDate.LastDateOfMonth();

            var parameters = new[]
            {
                new Microsoft.Data.SqlClient.SqlParameter("@StartDate", primerDiaDelMes),
                new Microsoft.Data.SqlClient.SqlParameter("@EndDate", ultimoDiaDelMes) 
            };

            var result = _genericRepository.ExecuteQuery<TicketsInfoDTO>(parameters, query);

            return result.ToList();
        }
        private List<TicketsInfoDTO> GetActiveTickets()
        {
            var query = @"SELECT Estado, CAST(COUNT(*) AS VARCHAR(20)) AS Valor
                        FROM Ticket 
                        where Estado IN('Open','InProcess')
                        GROUP BY Estado";

            var parameters = new Microsoft.Data.SqlClient.SqlParameter[0];

            var result = _genericRepository.ExecuteQuery<TicketsInfoDTO>(parameters, query);

            return result.ToList();
        }
        public async Task<TicketDTO> UpdateTicketAsync(TicketRequest request)
        {
            var ticketId = request.Ticket.TicketId;
            var (ticket, validationMessage) = await ValidarTicketAsync(ticketId);

            if (validationMessage.HasValitationMessage())
            {
                return new TicketDTO
                {
                    Message = validationMessage.Message
                };
            }

            ticket.Prioridad = (int)request.Ticket.Prioridad;
            ticket.Estado = request.Ticket.Estado;
            ticket.AsignarUsuario(request.Ticket.AsignadoAUsuario);

            CommitTransaction(request.RequestUserInfo, "EditarTicket");
            return new TicketDTO();
        }
        public async Task<TicketDTO> GetTicketAsync(TicketRequest request)
        {
            List<string> includes = ["TicketComment"];
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
                FechaTransaccion = x.FechaTransaccion,
                ModificadoPor = x.ModificadoPor,
            }).ToList();
        }
        public async Task<TicketCommentDTO> CreateComment(TicketCommentRequest request)
        {
            string ticketId = request.TicketComment.TicketId;
            var(ticket, validationMessage) = await ValidarTicketAsync(ticketId);

            if (validationMessage.IsNotNull() && validationMessage.IsNotNull() && validationMessage.HasValitationMessage())
            {
                return new TicketCommentDTO
                {
                    Message = validationMessage.Message
                };
            }

            TicketComment nuevoComentario = new()
            {
                TicketId = ticket.TicketId,
                Comentario = request.TicketComment.Comentario,
            };

            _genericRepository.Add(nuevoComentario);

            CommitTransaction(request.RequestUserInfo, "CrearComentario");
            return new TicketCommentDTO();
        }
        private async Task<(Ticket? Ticket, TicketDTO? Error)> ValidarTicketAsync(string ticketId)
        {
            List<string> includes = ["TicketComment"];
            var ticket = await _genericRepository.GetSingleAsync<Ticket>(r => r.TicketId == ticketId, includes);

            if (ticket.IsNull())
                return (null, new TicketDTO { Message = $"El ticket {ticketId} no existe" });

            if (ticket.IsClosed())
                return (null, new TicketDTO { Message = $"El ticket {ticketId} ya está cerrado" });

            return (ticket, null);
        }

        public async Task<TicketDTO> CerrarTicket(TicketRequest request) 
        {
            var ticketId = request.Ticket.TicketId;
            var (ticket, validationMessage) = await ValidarTicketAsync(ticketId);

            var ticketReturn = new TicketDTO();

            if (validationMessage.IsNotNull() && validationMessage.HasValitationMessage())
            {
                ticketReturn.AppendValidationErrorMessage(validationMessage.Message);
                return ticketReturn;
            }

            if (!ticket.TieneAsignadoAgenteSoporte())
            {
                ticketReturn.AppendValidationErrorMessage("Asigne un agente de soporte, para cerrar el ticket");
                return ticketReturn;
            }

            if (!ticket.TieneComentarioDeSolucion())
            {
                ticketReturn.AppendValidationErrorMessage("Es necesario agregar un comentario, con la descripcion de la solucion");
                return ticketReturn;
            }

            ticket.CerrarTicket();

            CommitTransaction(request.RequestUserInfo, "CerrarTicket");
            return ticketReturn;
        }

        private void CommitTransaction(RequestUserInfo user, string accion)
        {
            var transaction = user.CrearTransactionInfo(accion);
            _genericRepository.UnitOfWork.Commit(transaction);
        }

        public void Dispose()
        {
            _genericRepository.Dispose();
        }
    }
}
