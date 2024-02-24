
namespace abantu.mvc.Models;

public class Gerente : Funcionario
{
    public Funcionario Contratar(Funcionario novoFuncionario)
    {
        throw new NotImplementedException();
    }

    public Funcionario Demitir(Funcionario funcionario)
    {
        throw new NotImplementedException();
    }

    public Funcionario AumentarSalario(Funcionario funcionario, decimal novoSalario)
    {
        throw new NotImplementedException();
    }

    public override List<Funcionario> Listar()
    {
        throw new NotImplementedException();
    }
}
