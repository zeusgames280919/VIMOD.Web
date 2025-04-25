using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using VIMOD.Domain.Models;


namespace VIMOD.Infrastructure.Context
{
    public class VIMODDbContext : IdentityDbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalle> PedidoDetalles { get; set; }
        public DbSet<CarroCompras> CarritoCompras { get; set; }
        public DbSet<Carousel> Carouseles { get; set; }
        public DbSet<Certificado> Certificados { get; set; }
        public DbSet<ContenidoEducativo> ContenidosEducativos { get; set; }

        public VIMODDbContext(DbContextOptions<VIMODDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
/*
            // Prevenir eliminación en cascada por defecto
*/
            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;

            }
        }
    }

}
