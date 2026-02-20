namespace WebApiClinicaOdonto.Models
{
    //O T significa que é genérico, ou seja, pode ser qualquer tipo de dado.
    //? -> pode ser nulo

    public class ResponseModel<T>
    {
        public T? Dados { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
}
