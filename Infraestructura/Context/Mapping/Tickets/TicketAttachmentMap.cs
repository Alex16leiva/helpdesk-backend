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
    internal class TicketAttachmentMap : EntityMap<TicketAttachment>
    {
        public override void Configure(EntityTypeBuilder<TicketAttachment> builder)
        {
            builder.HasKey(t => t.Id);
            builder.ToTable("TicketAttachment", "dbo");
            builder.Property(r => r.Id).HasColumnName("id");
            builder.Property(r => r.TicketId).HasColumnName("TicketId");
            builder.Property(r => r.FileName).HasColumnName("FileName");
            builder.Property(r => r.FilePath).HasColumnName("FilePath");
            builder.Property(r => r.Size).HasColumnName("Size");
            base.Configure(builder);
        }
    }
}
