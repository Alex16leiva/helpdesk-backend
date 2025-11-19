using Dominio.Context.Entidades.BConocimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Context.Mapping.BConocimiento
{
    internal class BaseConocimientoCategoriaMap : EntityMap<BaseConocimientoCategoria>
    {
        public override void Configure(EntityTypeBuilder<BaseConocimientoCategoria> builder)
        {
            builder.HasKey(r => r.Id);
            builder.ToTable("BaseConocimientoCategoria");
            builder.Property(r => r.Id).HasColumnName("Id").IsRequired();
            builder.Property(r => r.Nombre).HasColumnName("Nombre").IsRequired().HasMaxLength(100);
            builder.Property(r => r.Descripcion).HasColumnName("Descripcion").HasMaxLength(250);
            base.Configure(builder);
        }
    }
}
