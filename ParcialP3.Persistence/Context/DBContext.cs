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
        public DbSet<MStatus> mStatus { get; set; }
    }
}
