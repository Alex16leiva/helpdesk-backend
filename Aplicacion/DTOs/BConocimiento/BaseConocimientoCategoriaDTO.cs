namespace Aplicacion.DTOs.BConocimiento
{
    public class BaseConocimientoCategoriaDTO : ResponseBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
