using EF_Curso.Data;
using EF_Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EF_Curso
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.Local.json", optional: false)
                .Build();

            string? connectionString =
                configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string não encontrada.");
            }

            /*
            using var context = new ApplicationContext(connectionString);
            var existeAtualizacao = context.Database.GetPendingMigrations();

            if(!existeAtualizacao.IsNullOrEmpty())
            {
                //valida se executa ou nao as migrations
            }*/

            //Inserindo dados na inicializacao
            /*var produto = new Produto
            {
                Descricao = "Produto para teste",
                CodigoBarras = "134135413513",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };*/

            try
            {
                //using var produtoContext = new ApplicationContext(connectionString);
                //produtoContext.produtos.Add(produto);
                //produtoContext.Set<Produto>().Add(produto);
                //produtoContext.Entry(produto).State = EntityState.Added;
                //produtoContext.Add(produto);
                //var registros = produtoContext.SaveChanges(); //aplica as alteracoes no banco
                //Console.WriteLine(string.Format("{0} registros inseridos !", registros));
                //InserirDados(connectionString);
                //ConsultarDados(connectionString);
                //CadastrarPedido(connectionString);
                ConstularPedidoCarregamentoAdiantado(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Erro {0}.", ex.Message));
            }

        }

        private static void InserirDados(string connection)
        {
            //Inserindo varios itens individuais
            /*
            var produto = new Produto
            {
                Descricao = "Produto para teste",
                CodigoBarras = "134135413513",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Fulano teste",
                CEP = "00000000",
                Cidade = "São Paulo",
                Estado = "SP",
                Telefone = "11912345678",
                Email = "email@teste.com.br"
            };

            using var context = new ApplicationContext(connectionString);
            context.AddRange(produto, cliente);
            var registros = context.SaveChanges();
            Console.WriteLine($"Registros: {registros}");
            */

            //Inserindo em lista
            var listaClientes = new[]
            {
                new Cliente
                {
                    Nome = "Fulano teste1",
                    CEP = "00000000",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Telefone = "11912345678",
                    Email = "email@teste.com.br"
                },

                new Cliente
                {
                    Nome = "Ciclano teste",
                    CEP = "11111111",
                    Cidade = "Rio de Janeiro",
                    Estado = "RJ",
                    Telefone = "11987654321",
                    Email = "outroemail@teste.com.br"
                }
            };

            using var context = new ApplicationContext(connection);
            context.AddRange(listaClientes);
            var registros = context.SaveChanges();
            Console.WriteLine($"Registros: {registros}");
        }

        private static void ConsultarDados(string connection)
        {
            using var context = new ApplicationContext(connection);
            //var consultaViaSintaxe = (from c in context.clientes where c.Id > 0 select c).ToList();
            //var consultaViaMetodo = context.clientes.Where(c => c.Id > 0 ).ToList(); //rastreia objetos em memoria
            var consultaViaMetodo = context.clientes.AsNoTracking()
                .Where(c => c.Id > 0)
                .OrderBy(c => c.Id)
                .ToList(); //Não rastreia objetos em memoria

            foreach (var cliente in consultaViaMetodo)
            {
                //Busca em memoria, se nao encontrar, busca na base de dados
                Console.WriteLine($"Consultando cliente: {cliente.Id}");
                //context.clientes.Find(cliente.Id); //Apenas o Find() consulta a memoria, os demais consultam o banco de dados
                context.clientes.FirstOrDefault(c => c.Id == cliente.Id);
            }
        }

        private static void CadastrarPedido(string connection)
        {
            using var context = new ApplicationContext(connection);
            var cliente = context.clientes.FirstOrDefault();
            var produto = context.produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEM = DateTime.Now,
                Observacao = "Pedido teste",
                StatusPedido = ValueObjects.StatusPedido.Analise,
                TipoFrete = ValueObjects.TipoFrete.SemFrete,
                Items = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10
                    }
                }
            };

            context.pedidos.Add(pedido);
            context.SaveChanges();

        }

        private static void ConstularPedidoCarregamentoAdiantado(string connection)
        {
            using var context = new ApplicationContext(connection);
            var pedidos = context
                .pedidos
                .Include(pedido => pedido.Items)
                .ThenInclude(item => item.Produto)
                .ToList();

            foreach (var item in pedidos)
                Console.WriteLine(item.Id + " - " + item.StatusPedido); 
                
        }

    }
}




