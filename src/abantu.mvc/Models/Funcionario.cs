using abantu.mvc.Data;

namespace abantu.mvc.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public decimal Salario { get; set; }
        public Cargo Cargo { get; set; }
        public List<Avaliacao> Avaliacoes { get; set; }

        protected ApplicationDbContext _db;

        public Funcionario(ApplicationDbContext db)
        {
            _db = db;
        }

        public virtual List<Funcionario> Listar()
        {
            return Listar(true);
        }

        protected List<Funcionario> Listar(bool somenteAtivos)
        {
            // Variável do tipo lista de funcionários que vai armazenar o retorno do método
            List<Funcionario> funcionarios;
            // Verificação se devemos filtrar somente funcionários ativos
            if (somenteAtivos)
                // Filtro de funcionários ativos
                funcionarios = _db.Funcionarios.Where(funcionario => funcionario.Ativo == true).ToList();
            else
                // Listagem sem filtro
                funcionarios = _db.Funcionarios.ToList();
            // Retorno da função
            return funcionarios;
        }
    }
}