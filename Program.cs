using EF_Curso.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using EF_Curso.Domain;

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
    
    }
    catch (Exception ex)
    {
        Console.WriteLine(string.Format("Erro ao inserir registro(s) {0}.", ex.Message));
    }


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

using var context = new ApplicationContext(connectionString);
context.AddRange(listaClientes);
var registros = context.SaveChanges();
Console.WriteLine($"Registros: {registros}");



