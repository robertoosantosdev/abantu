using System.ComponentModel.DataAnnotations;

namespace abantu.mvc.Models
{
    public class Cargo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do cargo.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o nível do cargo.")]
        [Display(Name = "Nível")]
        public int Nivel { get; set; }
    }
}