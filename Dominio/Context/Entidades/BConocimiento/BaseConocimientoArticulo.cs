using Dominio.Core;

namespace Dominio.Context.Entidades.BConocimiento
{
    public class BaseConocimientoArticulo : Entity
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int CategoriaId { get; set; }
        public string Tags { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int ContadorDeVistas { get; set; }
        public bool EsDestacado { get; set; }
        public BaseConocimientoCategoria Categoria { get; set; }
    }
}
