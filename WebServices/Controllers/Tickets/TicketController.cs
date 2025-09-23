using Aplicacion.Services.Tickets;
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
    }
}
