
using abantu.mvc.Data;

namespace abantu.mvc.Models;

public class Gerente : Funcionario
{
    public Gerente(ApplicationDbContext db) : base(db) {

    }
    
    public Funcionario Contratar(Funcionario novoFuncionario)
    {
        _db.Add(novoFuncionario);
        _db.SaveChanges();
        return novoFuncionario;
    }

    public Funcionario Demitir(Funcionario funcionario)
    {
        decimal mediaMinima = 5;

        Funcionario funcionarioDb = _db.Funcionarios.Single(f => f.Id == funcionario.Id);

        if (funcionarioDb.Avaliacoes == null || funcionarioDb.Avaliacoes.Count <= 0) {
            throw new ApplicationException("Funcionário não possui avaliações. É necessário uma média inferior a 5 para realizar uma demissão");
        }

        if (CalcularMediaAvaliacoes(funcionarioDb.Avaliacoes) < mediaMinima) {

            funcionarioDb.Ativo = false;
            _db.SaveChanges();
        }

        return funcionarioDb;
    }

    
    public Funcionario AumentarSalario(Funcionario funcionario, decimal novoSalario)
    {
        decimal mediaMinima = 7;

        Funcionario funcionarioDb = _db.Funcionarios.Single(f => f.Id == funcionario.Id);

        if (funcionarioDb.Avaliacoes == null || funcionarioDb.Avaliacoes.Count == 0) {
            throw new ApplicationException("Funcionário não possui avaliações. É necessário uma média superior a 7 para realizar um aumento.");
        }

        if (CalcularMediaAvaliacoes(funcionarioDb.Avaliacoes) > mediaMinima) {

            if (funcionarioDb.Salario >= novoSalario)
                throw new ApplicationException(string.Format("O novo salário: {1}, deve ser maior que o salário atual: {0}.", funcionarioDb.Salario, novoSalario));

            funcionarioDb.Salario = novoSalario;
            _db.SaveChanges();
        }

        return funcionarioDb;
    }

    public override List<Funcionario> Listar()
    {
        return Listar(false);
    }

    private decimal CalcularMediaAvaliacoes(List<Avaliacao> avaliacoes){

        var quantidade = avaliacoes.Count;
        decimal total = 0;
        decimal media = 0;

        for (int i = 0; i < quantidade; i++)
        {
            total += avaliacoes[i].Nota;
        }

        media = total / quantidade;

        return media;
    }
}
