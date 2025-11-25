using Aplicacion.DTOs.BConocimiento;
using Aplicacion.Services.BConocimiento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebServices.Controllers.BConocimiento
{
    [Route("api/categoria")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private readonly CategoriaAppService _categoriaService;

        public CategoriaController(CategoriaAppService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CrearCategoria([FromBody] BaseConocimientoCategoriaRequest request)
        {
            var result = _categoriaService.CrearCategoriaAsync(request);
            return Ok(result);
        }

        [HttpPost("get-categories")]
        [Authorize]
        public IActionResult ObtenerCategorias(BaseConocimientoCategoriaRequest request)
        {
            var result = _categoriaService.ObtenerCategoriasAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> ObtenerCategoriaPorId(int id)
        {
            var result = await _categoriaService.ObtenerCategoriaPorIdAsync(id);
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ActualizarCategoria([FromBody] BaseConocimientoCategoriaRequest request)
        {
            var result = await _categoriaService.ActualizarCategoriaAsync(request);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> EliminarCategoria([FromBody] BaseConocimientoCategoriaRequest request)
        {
            var result = await _categoriaService.EliminarCategoriaAsync(request);
            return Ok(result);
        }
    }
}