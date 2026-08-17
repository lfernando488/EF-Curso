
using EF_Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Curso.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(client => client.Id);
            builder.Property(cliente => cliente.Nome).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(cliente => cliente.Telefone).HasColumnType("CHAR(11)");
            builder.Property(cliente => cliente.CEP).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(cliente => cliente.Estado).HasColumnType("CHAR(2)").IsRequired();
            builder.Property(cliente => cliente.Cidade).HasMaxLength(60).IsRequired();

            builder.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
        }
    }
}
