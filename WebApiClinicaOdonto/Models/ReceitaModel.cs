using System.Text.Json.Serialization;

namespace WebApiClinicaOdonto.Models
{
    public class ReceitaModel
    {
        public int Id { get; set; }
        public int ConsultaId { get; set; }
        public string Prescricao { get; set; }
        public string Remedio { get; set; }

        [JsonIgnore]
        public ConsultaModel Consulta { get; set; }
    }
}
