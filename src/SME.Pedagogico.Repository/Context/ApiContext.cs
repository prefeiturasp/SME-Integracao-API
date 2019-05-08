using Microsoft.EntityFrameworkCore;
using SME.Pedagogico.Interface.Logs;
using SME.Pedagogico.Repository.Context.Configurations;

namespace SME.Pedagogico.Repository.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        public DbSet<Logger> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogApiConfiguration());
        }
    }
}
