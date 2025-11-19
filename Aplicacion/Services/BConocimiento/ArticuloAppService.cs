using Aplicacion.DTOs;
using Aplicacion.DTOs.BConocimiento;
using Aplicacion.Helpers;
using Dominio.Context.Entidades.BConocimiento;
using Dominio.Core;
using Dominio.Core.Extensions;
using Infraestructura.Context;

namespace Aplicacion.Services.BConocimiento
{
    public class ArticuloAppService
    {
        private readonly IGenericRepository<IDataContext> _repositorio;

        public ArticuloAppService(IGenericRepository<IDataContext> repositorio)
        {
            _repositorio = repositorio;
        }

        public BaseConocimientoArticuloDTO CrearArticuloAsync(BaseConocimientoArticuloRequest request)
        {
            var articulo = new BaseConocimientoArticulo.Builder()
                .ConTitulo(request.Articulo.Titulo)
                .ConContenido(request.Articulo.Contenido)
                .ConTags(request.Articulo.Tags)
                .ConDestacado(request.Articulo.EsDestacado)
                .ConCategoriaId(request.Articulo.CategoriaId)
                .Build();

            _repositorio.Add(articulo);
            TransactionInfo transactionInfo = request.RequestUserInfo.CrearTransactionInfo("CrearArticulo");
            _repositorio.UnitOfWork.Commit(transactionInfo);

            return request.Articulo;
        }

        public SearchResult<BaseConocimientoArticuloDTO> ObtenerArticulosAsync(BaseConocimientoArticuloRequest request)
        {
            var dynamicFilter = DynamicFilterFactory.CreateDynamicFilter(request.QueryInfo);
            var articulos = _repositorio.GetPagedAndFiltered<BaseConocimientoArticulo>(dynamicFilter);

            return new SearchResult<BaseConocimientoArticuloDTO>
            {
                PageCount = articulos.PageCount,
                ItemCount = articulos.ItemCount,
                TotalItems = articulos.TotalItems,
                PageIndex = articulos.PageIndex,
                Items = (from qry in articulos.Items as IEnumerable<BaseConocimientoArticulo>
                         select MapArticuloDto(qry)).ToList(),
            };
        }

        private static BaseConocimientoArticuloDTO MapArticuloDto(BaseConocimientoArticulo qry)
        {
            return new BaseConocimientoArticuloDTO
            {
                Id = qry.Id,
                Titulo = qry.Titulo,
                Contenido = qry.Contenido,
                Tags = qry.Tags,
                CategoriaId = qry.CategoriaId
            };
        }

        public async Task<BaseConocimientoArticuloDTO> ObtenerArticuloPorIdAsync(int id)
        {
            var articulo = await _repositorio.GetSingleAsync<BaseConocimientoArticulo>(a => a.Id == id);

            return articulo.IsNotNull() ? MapArticuloDto(articulo) : new BaseConocimientoArticuloDTO();
        }

        public async Task<BaseConocimientoArticuloDTO> ActualizarArticuloAsync(BaseConocimientoArticuloRequest request)
        {
            var articulo = await _repositorio.GetSingleAsync<BaseConocimientoArticulo>(x => x.Id == request.Articulo.Id);
            if (articulo.IsNull())
            {
                return new BaseConocimientoArticuloDTO
                {
                    Message = $"El artículo con Id {request.Articulo.Id} no existe"
                };
            }

            articulo = new BaseConocimientoArticulo.Builder(articulo)
                .ConTitulo(request.Articulo.Titulo)
                .ConContenido(request.Articulo.Contenido)
                .ConTags(request.Articulo.Tags)
                .ConCategoriaId(request.Articulo.CategoriaId)
                .Build();

            TransactionInfo transactionInfo = request.RequestUserInfo.CrearTransactionInfo("ActualizarArticulo");
            _repositorio.UnitOfWork.Commit(transactionInfo);

            return MapArticuloDto(articulo);
        }

        public async Task<BaseConocimientoArticuloDTO> EliminarArticuloAsync(BaseConocimientoArticuloRequest request)
        {
            var articulo = await _repositorio.GetSingleAsync<BaseConocimientoArticulo>(x => x.Id == request.Articulo.Id);
            if (articulo.IsNull())
            {
                return new BaseConocimientoArticuloDTO
                {
                    Message = $"El artículo con Id {request.Articulo.Id} no existe"
                };
            }

            //articulo.Activo = false;

            TransactionInfo transactionInfo = request.RequestUserInfo.CrearTransactionInfo("EliminarArticulo");
            _repositorio.UnitOfWork.Commit(transactionInfo);

            return new BaseConocimientoArticuloDTO();
        }
    }
}