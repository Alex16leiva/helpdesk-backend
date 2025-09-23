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
            builder.Property(t => t.TicketId).HasColumnName("Ticket");
            builder.Property(t => t.Title).HasColumnName("Title");
            builder.Property(t =>t.Description).HasColumnName("Description");
            builder.Property(t => t.Priority).HasColumnName("Priority");
            builder.Property(t => t.Status).HasColumnName("Status");
            builder.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            builder.Property(t => t.AssignedToUserId).HasColumnName("AssignedToUserId");
            builder.Property(t => t.DateCreated).HasColumnName("DateCreated");
            builder.Property(t => t.DateClosed).HasColumnName("DateClosed");
            base.Configure(builder);
        }
    }
}
