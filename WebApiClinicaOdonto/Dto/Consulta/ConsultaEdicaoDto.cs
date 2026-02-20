namespace WebApiClinicaOdonto.Dto.Consulta
{
    public class ConsultaEdicaoDto
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int DentistaId { get; set; }
        public DateTime Data_Horario { get; set; }
        public string Sintoma { get; set; }
        public string Tratamento { get; set; }
        public string Procedimento { get; set; }
    }
}
