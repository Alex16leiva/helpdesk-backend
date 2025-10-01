using Aplicacion.DTOs.Tickets;
using Aplicacion.Services.Tickets;
using Dominio.Context.Entidades.Tickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebServices.Controllers.Tickets
{
    [Route("api/ticket")]
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
    }
}
