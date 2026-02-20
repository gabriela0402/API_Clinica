using System.ComponentModel.DataAnnotations;

namespace WebApiClinicaOdonto.Models
{
    public class ConsultaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O paciente é obrigatório.")]
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "O dentista é obrigatório.")]
        public int DentistaId { get; set; }

        [Required(ErrorMessage = "A data e o horário são obrigatórios.")]
        public DateTime Data_Horario { get; set; }
        
        public string Sintoma { get; set; }
        public string Tratamento { get; set; }
        public string Procedimento { get; set; }


        public PacienteModel Paciente { get; set; }
        public DentistaModel Dentista { get; set; }

        public ICollection<ReceitaModel> Receitas { get; set; }
    }
}
