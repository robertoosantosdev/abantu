using abantu.mvc.Data;
using abantu.mvc.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace abantu.tests;

public class FuncionarioTest
{
    // Variável interna que compartilha o banco entre os testes.
    protected ApplicationDbContext _db;
    // Construtor que constroi nosso cenario.
    public FuncionarioTest()
    {
        // Aqui, criamos um banco falso (mock) em memória. Ou seja,
        // assim que o teste termina, ele desaparece.
        var connection = new SqliteConnection("Filename=:memory:");
        // Essa função abre a conexão com o banco.
        connection.Open();
        // O EF precisa de algumas opções de contexto para ser criado.
        // Usamos este Builder para nos ajudar no processo.
        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connection)
            .Options;
        // Aqui iniciamos o EF com nosso banco de testes
        _db = new ApplicationDbContext(contextOptions);
        // Esta função garante que nossas tabelas existam no banco
        _db.Database.EnsureCreated();
        // Criamos um cargo de Vendedor somente para estes testes
        var cargoVendedor = new Cargo();
        cargoVendedor.Nome = "Vendedor";
        cargoVendedor.Nivel = 0;
        // Criamos um funcionário fictício
        var juarez = new Funcionario(_db);
        juarez.Nome = "Juarez Soares";
        juarez.Salario = 1000;
        juarez.Cargo = cargoVendedor;
        // Criamos um funcionário INATIVO fictício
        var inativo = new Funcionario(_db);
        inativo.Nome = "Jorge Matos";
        inativo.Salario = 900;
        inativo.Ativo = false;
        inativo.Cargo = cargoVendedor;
        // Adicionamos o funcionário ao banco de dados
        _db.Funcionarios.Add(juarez);
        _db.Funcionarios.Add(inativo);
        // Salvamos as alterações
        _db.SaveChanges();
    }

    [Fact]
    public virtual void ListarTest()
    {
        var expected = 1;
        Funcionario funcionario = new Funcionario(_db);
        List<Funcionario> funcionarios = funcionario.Listar();
        var actual = funcionarios.Count;

        Assert.Equal(expected, actual);
    }
}