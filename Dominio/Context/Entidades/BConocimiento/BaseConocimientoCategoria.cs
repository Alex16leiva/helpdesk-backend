using Dominio.Core;

namespace Dominio.Context.Entidades.BConocimiento
{
    public class BaseConocimientoCategoria : Entity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public ICollection<BaseConocimientoArticulo> Articulos { get; set; }

    }
}
