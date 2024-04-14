using abantu.mvc.Models;

namespace abantu.tests;

public class GerenteTest : FuncionarioTest
{
    public GerenteTest() : base()
    {
        // Criamos o cargo fictício de gerente
        var cargoGerente = new Cargo();
        cargoGerente.Nome = "Gerente";
        cargoGerente.Nivel = 1;
        // Criamos um gerente fictício
        var maria = new Gerente(_db);
        maria.Nome = "Maria Antunes";
        maria.Salario = 2000;
        maria.Cargo = cargoGerente;
        // Adicionamos ao banco criado na classe mãe
        _db.Funcionarios.Add(maria);
        // Salvamos
        _db.SaveChanges();
    }

    [Fact]
    public override void ListarTest()
    {
        var expected = 3;

        Gerente gerente = _db.Gerentes.First();
        List<Funcionario> funcionarios = gerente.Listar();

        int actual = funcionarios.Count;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ContratarTest()
    {
        var expected = 4;
        // Procuramos pelo primeiro gerente
        Gerente gerente = _db.Gerentes.First();
        // Procuramos pelo cargo de Vendedor.
        Cargo cargoVendedor = _db.Cargos.First(c => c.Nome == "Vendedor");
        var novoFuncionario = new Funcionario(_db);
        novoFuncionario.Nome = "Roberto Lira";
        novoFuncionario.Cargo = cargoVendedor;
        novoFuncionario.Salario = 1000;
        gerente.Contratar(novoFuncionario);
        var actual = novoFuncionario.Id;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NaoDemitirTest()
    {
        Gerente gerente = _db.Gerentes.First();
        Funcionario funcionario = _db.Funcionarios.First(f => f.Ativo);
        Assert.Throws<ApplicationException>(() => gerente.Demitir(funcionario));
    }

    [Fact]
    public void DemitirTest()
    {
        var expected = false;
        Gerente gerente = _db.Gerentes.First();
        Funcionario funcionario = _db.Funcionarios.First(f => f.Ativo == true);
        Avaliacao avaliacao6 = new Avaliacao();
        avaliacao6.Avaliado = funcionario;
        avaliacao6.Avaliador = gerente;
        avaliacao6.Nota = 6;
        avaliacao6.RealizadaEm = DateTime.Now;
        avaliacao6.Comentario = "Razoável";
        Avaliacao avaliacao3 = new Avaliacao();
        avaliacao3.Avaliado = funcionario;
        avaliacao3.Avaliador = gerente;
        avaliacao3.Nota = 3;
        avaliacao3.RealizadaEm = DateTime.Now;
        avaliacao3.Comentario = "Ruim";
        funcionario.Avaliacoes = new List<Avaliacao>();
        funcionario.Avaliacoes.Add(avaliacao6);
        funcionario.Avaliacoes.Add(avaliacao3);
        _db.SaveChanges();
        funcionario = gerente.Demitir(funcionario);
        var actual = funcionario.Ativo;
        Assert.Equal(expected, actual);
    }


}