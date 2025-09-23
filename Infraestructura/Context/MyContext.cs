using Dominio.Context.Entidades.Seguridad;
using Dominio.Context.Entidades.Tickets;
using Dominio.Core;
using Infraestructura.Context.Mapping.Seguridad;
using Infraestructura.Context.Mapping.Tickets;
using Infraestructura.Core;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Context
{
    public class MyContext : BCUnitOfWork, IDataContext
    {
        public MyContext(DbContextOptions<MyContext> context)
            : base(context)
        {
            Database.SetCommandTimeout((int)TimeSpan.FromSeconds(1).TotalSeconds);
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Rol> Rol {  get; set; }
        public virtual DbSet<Pantalla> Pantalla { get; set; }
        public virtual DbSet<Permisos> Permisos { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<TicketAttachment> TicketAttachment { get; set; }
        public virtual DbSet<TicketComment> TicketComment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new PantallaMap());
            modelBuilder.ApplyConfiguration(new PermisosMap());
            modelBuilder.ApplyConfiguration(new TicketMap());
            modelBuilder.ApplyConfiguration(new TicketCommentMap());

            base.OnModelCreating(modelBuilder);
        }


        public override void Commit(TransactionInfo transactionInfo)
        {
            base.Commit(transactionInfo);
        }
    }
}
