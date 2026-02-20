using System.ComponentModel.DataAnnotations;
using WebApiClinicaOdonto.Models;

namespace WebApiClinicaOdonto.Dto.Consulta
{
    public class ConsultaCriacaoDto
    {
        public int PacienteId { get; set; }
        public int DentistaId { get; set; }
        public DateTime Data_Horario { get; set; }
        public string Sintoma { get; set; }
        public string Tratamento { get; set; }
        public string Procedimento { get; set; }

    }
}
