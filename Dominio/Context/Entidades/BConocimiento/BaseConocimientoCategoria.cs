using Dominio.Core;

namespace Dominio.Context.Entidades.BConocimiento
{
    public class BaseConocimientoCategoria : Entity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public ICollection<BaseConocimientoArticulo> Articulos { get; set; }

        public class Builder
        {
            private readonly BaseConocimientoCategoria _categoria;
            public Builder(BaseConocimientoCategoria existing)
            {
                _categoria = existing;
                _categoria.Articulos ??= new List<BaseConocimientoArticulo>();
            }
            public Builder()
            {
                _categoria = new BaseConocimientoCategoria
                {
                    Articulos = new List<BaseConocimientoArticulo>()
                };
            }

            public Builder ConId(int id)
            {
                _categoria.Id = id;
                return this;
            }

            public Builder ConNombre(string nombre)
            {
                _categoria.Nombre = nombre;
                return this;
            }

            public Builder ConDescripcion(string descripcion)
            {
                _categoria.Descripcion = descripcion;
                return this;
            }

            public Builder ConArticulo(BaseConocimientoArticulo articulo)
            {
                _categoria.Articulos.Add(articulo);
                return this;
            }

            public BaseConocimientoCategoria Build()
            {
                return _categoria;
            }
        }
    }
}
