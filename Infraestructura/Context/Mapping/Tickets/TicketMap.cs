using Dominio.Context.Entidades.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Context.Mapping.Tickets
{
    internal class TicketMap : EntityMap<Ticket>
    {
        public override void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.TicketId);
            builder.ToTable("Ticket","dbo");
            builder.Property(t => t.TicketId).HasColumnName("TicketId");
            builder.Property(t => t.Titulo).HasColumnName("Titulo");
            builder.Property(t =>t.Descripcion).HasColumnName("Descripcion");
            builder.Property(t => t.Prioridad).HasColumnName("Prioridad");
            builder.Property(t => t.Estado).HasColumnName("Estado");
            builder.Property(t => t.CreadoPor).HasColumnName("CreadoPor");
            builder.Property(t => t.AsignadoAUsuario).HasColumnName("AsignadoAUsuario");
            builder.Property(t => t.FechaCreado).HasColumnName("FechaCreado");
            builder.HasMany(t => t.TicketComment).WithOne(t => t.Ticket).HasForeignKey(t => t.TicketId);
            base.Configure(builder);
        }
    }
}
