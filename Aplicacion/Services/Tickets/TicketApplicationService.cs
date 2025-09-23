using Infraestructura.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Services.Tickets
{
    public class TicketApplicationService
    {
        private readonly IGenericRepository<IDataContext> _genericRepository;

        public TicketApplicationService(IGenericRepository<IDataContext> genericRepository)
        {
            genericRepository = _genericRepository;
        }


    }
}
