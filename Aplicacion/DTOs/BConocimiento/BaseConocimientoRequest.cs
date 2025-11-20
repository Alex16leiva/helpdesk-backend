namespace Aplicacion.DTOs.BConocimiento
{
    public class BaseConocimientoCategoriaRequest : RequestBase
    {
        public BaseConocimientoCategoriaDTO? Categoria { get; set; }
    }

    public class BaseConocimientoArticuloRequest : RequestBase
    {
        public BaseConocimientoArticuloDTO? Articulo { get; set; }
    }
}
