using EF_Curso.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

using var context = new ApplicationContext(connectionString);
var existeAtualizacao = context.Database.GetPendingMigrations();

if(!existeAtualizacao.IsNullOrEmpty())
{
    //valida se executa ou nao as migrations
}

Console.WriteLine("Aplicação inciada!");