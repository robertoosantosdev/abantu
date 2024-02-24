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

        public virtual List<Funcionario> Listar() {
            throw new NotImplementedException();
        }

        protected List<Funcionario> Listar(bool somenteAtivos) {
            throw new NotImplementedException();
        }
    }
}