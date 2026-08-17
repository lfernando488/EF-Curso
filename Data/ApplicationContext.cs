
using EF_Curso.Domain;
using Microsoft.EntityFrameworkCore;

namespace EF_Curso.Data
{
    public class ApplicationContext : DbContext
    {

        private readonly string _connectionString;

        public DbSet<Pedido> pedidos { get; set; }

        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(cliente =>
            {
                cliente.ToTable("Clientes");
                cliente.HasKey(client => client.Id);
                cliente.Property(cliente => cliente.Nome).HasColumnType("VARCHAR(80)").IsRequired();
                cliente.Property(cliente => cliente.Telefone).HasColumnType("CHAR(11)");
                cliente.Property(cliente => cliente.CEP).HasColumnType("CHAR(8)").IsRequired();
                cliente.Property(cliente => cliente.Estado).HasColumnType("CHAR(2)").IsRequired();
                cliente.Property(cliente => cliente.Cidade).HasMaxLength(60).IsRequired();
                cliente.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
            });

            modelBuilder.Entity<Produto>(produto =>
            {
                produto.ToTable("Produtos");
                produto.HasKey(produto => produto.Id);
                produto.Property(produto => produto.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
                produto.Property(produto => produto.Descricao).HasColumnType("CARCHAR(60)");
                produto.Property(produto => produto.Valor).IsRequired();
                produto.Property(produto => produto.TipoProduto).HasConversion<string>();
            });

            modelBuilder.Entity<Pedido>(pedido =>
            {
                pedido.ToTable("Pedidos");
                pedido.HasKey(pedido => pedido.Id);
                pedido.Property(pedido => pedido.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
                pedido.Property(pedido => pedido.StatusPedido).HasConversion<string>();
                pedido.Property(pedido => pedido.TipoFrete).HasConversion<int>();
                pedido.Property(pedido => pedido.Observacao).HasColumnType("VARCHAR(512)");

                pedido.HasMany(pedido => pedido.Items)
                    .WithOne(pedido => pedido.Pedido)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
