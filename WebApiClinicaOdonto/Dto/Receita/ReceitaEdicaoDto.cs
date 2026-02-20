namespace WebApiClinicaOdonto.Dto.Receita
{
    public class ReceitaEdicaoDto
    {
        public int Id { get; set; }
        public int ConsultaId { get; set; }
        public string Prescricao { get; set; }
        public string Remedio { get; set; }
    }
}
