using Parcial2_FlorezRSebastian.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Parcial2_FlorezRSebastian.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Ticket> Tickets { get; set; } //cada que se cree una entidad se debe crear un nuevo dbset, todas tablas en plural

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>().HasIndex(t => t.CodTicket).IsUnique(); //crear un modelo y se le aplicaun modelo y se le asigna que quiere, en este caso es que sea unico en la base de datos           
        }

    }
}
