using Microsoft.EntityFrameworkCore;
using ParcialP3.Domain.Entities;

namespace ParcialP3.Persistence.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Users> users { get; set; }
        public DbSet<mSTATUS> mSTATUS { get; set; }
        public DbSet<Ciudades> ciudades { get; set; }
        public DbSet<Inmuebles> inmuebles { get; set; }
        public DbSet<InmuebleImagenes> inmueblesImagenes { get; set; }
        public DbSet<TipoPropiedad> tipoPropiedad { get; set; }
    }
}
