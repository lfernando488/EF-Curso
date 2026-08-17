using EF_Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Curso.Data.Configurations
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(produto => produto.Id);
            builder.Property(produto => produto.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
            builder.Property(produto => produto.Descricao).HasColumnType("CARCHAR(60)");
            builder.Property(produto => produto.Valor).IsRequired();
            builder.Property(produto => produto.TipoProduto).HasConversion<string>();
        }
    }
}
