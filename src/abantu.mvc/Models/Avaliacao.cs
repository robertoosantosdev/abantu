using System.ComponentModel.DataAnnotations;

namespace abantu.mvc.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public Funcionario Avaliado { get; set; }
        public Gerente Avaliador { get; set; }
        public DateTime RealizadaEm { get; set; }
        [Range(1, 10, ErrorMessage = "A nota deve ser entre 1 e 10.")]
        public int Nota { get; set; }
        public string Comentario { get; set; }
    }

}