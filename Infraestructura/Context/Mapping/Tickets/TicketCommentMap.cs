using Dominio.Context.Entidades.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Context.Mapping.Tickets
{
    internal class TicketCommentMap : EntityMap<TicketComment>
    {
        public override void Configure(EntityTypeBuilder<TicketComment> builder)
        {
            builder.HasKey(t => t.Id);
            builder.ToTable("TicketComment", "dbo");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.TicketId).HasColumnName("TicketId");
            builder.Property(t => t.Comentario).HasColumnName("Comentario");
            base.Configure(builder);
        }
    }
}
