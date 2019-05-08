using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SME.Pedagogico.Interface.Logs;

namespace SME.Pedagogico.Repository.Context.Configurations
{
    internal class LogApiConfiguration : IEntityTypeConfiguration<Logger>
    {
        public void Configure(EntityTypeBuilder<Logger> configuration)
        {
            configuration.ToTable("Logs");

            configuration.HasKey(e => e.Guid);
            configuration.Property(e => e.Metodo).IsRequired();
            configuration.Property(e => e.Parametros);
            configuration.Property(e => e.Servico).IsRequired();
            configuration.Property(e => e.Usuario).IsRequired();
            configuration.Property(e => e.DataCriacao).IsRequired();
        }
    }
}
