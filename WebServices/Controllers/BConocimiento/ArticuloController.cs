using Aplicacion.DTOs.BConocimiento;
using Aplicacion.Services.BConocimiento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebServices.Controllers.BConocimiento
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : Controller
    {
        private readonly ArticuloAppService _articuloService;

        public ArticuloController(ArticuloAppService articuloService)
        {
            _articuloService = articuloService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CrearArticulo([FromBody] BaseConocimientoArticuloRequest request)
        {
            var result = _articuloService.CrearArticuloAsync(request);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ObtenerArticulos([FromQuery] BaseConocimientoArticuloRequest request)
        {
            var result = _articuloService.ObtenerArticulosAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> ObtenerArticuloPorId(int id)
        {
            var result = await _articuloService.ObtenerArticuloPorIdAsync(id);
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ActualizarArticulo([FromBody] BaseConocimientoArticuloRequest request)
        {
            var result = await _articuloService.ActualizarArticuloAsync(request);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> EliminarArticulo([FromBody] BaseConocimientoArticuloRequest request)
        {
            var result = await _articuloService.EliminarArticuloAsync(request);
            return Ok(result);
        }
    }
}
