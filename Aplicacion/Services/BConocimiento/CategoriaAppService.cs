using Aplicacion.DTOs;
using Aplicacion.DTOs.BConocimiento;
using Aplicacion.Helpers;
using Dominio.Context.Entidades.BConocimiento;
using Dominio.Context.Entidades.Seguridad;
using Dominio.Core;
using Dominio.Core.Extensions;
using Infraestructura.Context;

namespace Aplicacion.Services.BConocimiento
{
    public class CategoriaAppService
    {
        private readonly IGenericRepository<IDataContext> _repositorio;

        public CategoriaAppService(IGenericRepository<IDataContext> repositorio)
        {
            _repositorio = repositorio;
        }

        // Crear categoría
        public BaseConocimientoCategoriaDTO CrearCategoriaAsync(BaseConocimientoCategoriaRequest request)
        {
            var categoria = new BaseConocimientoCategoria.Builder()
                .ConNombre(request.Categoria.Nombre)
                .ConDescripcion(request.Categoria.Descripcion)
                .Build();

            _repositorio.Add(categoria);
            TransactionInfo transactionInfo = request.RequestUserInfo.CrearTransactionInfo("CrearCategoria");
            _repositorio.UnitOfWork.Commit(transactionInfo);
            return request.Categoria;
        }

        public SearchResult<BaseConocimientoCategoriaDTO> ObtenerCategoriasAsync(BaseConocimientoCategoriaRequest request)
        {
            var dynamicFilter = DynamicFilterFactory.CreateDynamicFilter(request.QueryInfo);
            var categoria = _repositorio.GetPagedAndFiltered<BaseConocimientoCategoria>(dynamicFilter);

            return new SearchResult<BaseConocimientoCategoriaDTO>
            {
                PageCount = categoria.PageCount,
                ItemCount = categoria.ItemCount,
                TotalItems = categoria.TotalItems,
                PageIndex = categoria.PageIndex,
                Items = (from qry in categoria.Items as IEnumerable<BaseConocimientoCategoria> select MapCategoriaDto(qry)).ToList(),
            };
        }

        private static BaseConocimientoCategoriaDTO MapCategoriaDto(BaseConocimientoCategoria qry)
        {
            return new BaseConocimientoCategoriaDTO
            {
                Id = qry.Id,
                Nombre = qry.Nombre,
                Descripcion = qry.Descripcion,
            };
        }

        public async Task<BaseConocimientoCategoriaDTO> ObtenerCategoriaPorIdAsync(int id)
        {
            var categoria = await _repositorio.GetSingleAsync<BaseConocimientoCategoria>(c => c.Id == id);
            
            return categoria.IsNotNull() ? MapCategoriaDto(categoria) : new BaseConocimientoCategoriaDTO();
        }

        public async Task<BaseConocimientoCategoriaDTO> ActualizarCategoriaAsync(BaseConocimientoCategoriaRequest request)
        {
            var categoria = await _repositorio.GetSingleAsync<BaseConocimientoCategoria>(x => x.Id == request.Categoria.Id);
            if (categoria.IsNull())
            {
                return new BaseConocimientoCategoriaDTO
                {
                    Message = $"La categoría con Id {request.Categoria.Id} no existe"
                };
            }

            categoria = new BaseConocimientoCategoria.Builder(categoria)
            .ConNombre(request.Categoria.Nombre)
            .ConDescripcion(request.Categoria.Descripcion)
            .Build();

            TransactionInfo transactionInfo = request.RequestUserInfo.CrearTransactionInfo("ActualizarCategoria");
            _repositorio.UnitOfWork.Commit(transactionInfo);

            return MapCategoriaDto(categoria);
        }

        public async Task<BaseConocimientoCategoriaDTO> EliminarCategoriaAsync(BaseConocimientoCategoriaRequest request)
        {
            var categoria = await _repositorio.GetSingleAsync<BaseConocimientoCategoria>(x => x.Id == request.Categoria.Id);
            if (categoria.IsNull())
            {
                return new BaseConocimientoCategoriaDTO
                {
                    Message = $"La categoría con Id {request.Categoria.Id} no existe"
                };
            }


            TransactionInfo transactionInfo = request.RequestUserInfo.CrearTransactionInfo("ActualizarCategoria");
            _repositorio.UnitOfWork.Commit(transactionInfo);

            return new BaseConocimientoCategoriaDTO();
        }
    }
}
