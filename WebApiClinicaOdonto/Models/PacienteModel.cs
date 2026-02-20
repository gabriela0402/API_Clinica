using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApiClinicaOdonto.Models
{
    public class PacienteModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do paciente é obrigatório.")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O CPF  do paciente é obrigatório.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O telefone do paciente é obrigatório.")]
        public string Telefone { get; set; }

        [JsonIgnore]
        public ICollection<ConsultaModel> Consultas { get; set; }
    }
}
