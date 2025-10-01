using Aplicacion.DTOs;
using Aplicacion.DTOs.Tickets;
using Aplicacion.Services.Tickets;
using Dominio.Context.Entidades.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebServices.Controllers.Tickets
{
    [Route("api/ticket/by-ticket-id")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketApplicationService _ticketApplicationService;

        public TicketController(TicketApplicationService ticketApplicationService)
        {
            _ticketApplicationService = ticketApplicationService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] TicketRequest ticket)
        {

            var result = await _ticketApplicationService.CreateTicketAsync(ticket);
            return Ok(result);
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetById(string ticketId)
        {
            TicketRequest request = new TicketRequest
            {
                Ticket = new TicketDTO
                {
                    TicketId = ticketId
                }
            };
            var result = await _ticketApplicationService.GetTicketAsync(request);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("get-paged")]
        public SearchResult<TicketDTO> GetTicketsPaged(TicketRequest request)
        {
            var result = _ticketApplicationService.GetAllTickets(request);
            return result;
        }

        [AllowAnonymous]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateTicketAsync(TicketRequest request)
        {
            var result = await _ticketApplicationService.UpdateTicketAsync(request);
            return Ok(result);
        }
    }
}
