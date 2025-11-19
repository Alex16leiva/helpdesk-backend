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

        // Clase Builder anidada
        public class Builder
        {
            private readonly BaseConocimientoArticulo _articulo;

            // Constructor vacío (para crear nuevo artículo)
            public Builder()
            {
                _articulo = new BaseConocimientoArticulo
                {
                    FechaCreacion = DateTime.UtcNow,
                    ContadorDeVistas = 0,
                    EsDestacado = false
                };
            }

            // Constructor con entidad existente (para actualizar)
            public Builder(BaseConocimientoArticulo existente)
            {
                _articulo = existente;
                _articulo.FechaCreacion = existente.FechaCreacion; // preserva fecha original
            }

            public Builder ConId(int id)
            {
                _articulo.Id = id;
                return this;
            }

            public Builder ConTitulo(string titulo)
            {
                _articulo.Titulo = titulo;
                return this;
            }

            public Builder ConContenido(string contenido)
            {
                _articulo.Contenido = contenido;
                return this;
            }

            public Builder ConCategoriaId(int categoriaId)
            {
                _articulo.CategoriaId = categoriaId;
                return this;
            }

            public Builder ConTags(string tags)
            {
                _articulo.Tags = tags;
                return this;
            }

            public Builder ConDestacado(bool esDestacado)
            {
                _articulo.EsDestacado = esDestacado;
                return this;
            }

            public Builder ConContadorDeVistas(int contador)
            {
                _articulo.ContadorDeVistas = contador;
                return this;
            }

            public Builder IncrementarVistas()
            {
                _articulo.ContadorDeVistas++;
                return this;
            }

            public BaseConocimientoArticulo Build()
            {
                return _articulo;
            }
        }
    }
}