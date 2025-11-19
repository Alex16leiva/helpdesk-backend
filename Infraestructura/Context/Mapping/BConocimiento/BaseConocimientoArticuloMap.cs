using Dominio.Context.Entidades.BConocimiento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Context.Mapping.BConocimiento
{
    internal class BaseConocimientoArticuloMap : EntityMap<BaseConocimientoArticulo>
    {
        public override void Configure(EntityTypeBuilder<BaseConocimientoArticulo> builder)
        {
            builder.HasKey(r => r.Id);
            builder.ToTable("BaseConocimientoArticulo");
            builder.Property(r => r.Id).HasColumnName("Id").IsRequired();
            builder.Property(r => r.Titulo).HasColumnName("Titulo").IsRequired().HasMaxLength(200);
            builder.Property(r => r.Contenido).HasColumnName("Contenido").IsRequired();
            builder.Property(r => r.CategoriaId).HasColumnName("CategoriaId").IsRequired();
            builder.Property(r => r.Tags).HasColumnName("Tags").HasMaxLength(100);
            builder.Property(r => r.FechaCreacion).HasColumnName("FechaCreacion").IsRequired();
            builder.Property(r => r.ContadorDeVistas).HasColumnName("ContadorDeVistas").IsRequired();
            builder.Property(r => r.EsDestacado).HasColumnName("EsDestacado").IsRequired();
            builder.HasOne(r => r.Categoria).WithMany(x => x.Articulos).HasForeignKey(r => r.CategoriaId);
            base.Configure(builder);
        }
    }
}
