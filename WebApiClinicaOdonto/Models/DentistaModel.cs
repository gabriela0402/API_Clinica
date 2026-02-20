using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApiClinicaOdonto.Models
{
    public class DentistaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do dentista é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A especialização do dentista é obrigatório.")]
        public string Especializacao { get; set; }

        [Required(ErrorMessage = "O telefone  do dentista é obrigatório.")]
        public string Telefone { get; set; }

        [JsonIgnore]
        public ICollection<ConsultaModel> Consultas { get; set; }
    }
}
