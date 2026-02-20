using WebApiClinicaOdonto.Dto.Consulta;
using WebApiClinicaOdonto.Models;

namespace WebApiClinicaOdonto.Services.Consulta
{
    public interface IConsultaInterface
    {
        Task<ResponseModel<List<ConsultaModel>>> ListarConsultas();
        Task<ResponseModel<List<ConsultaModel>>> ListarConsultasPorPacienteId(int idPaciente);
        Task<ResponseModel<List<ConsultaModel>>> ListarConsultasPorDentistaId(int idDentista);
        Task<ResponseModel<ConsultaModel>> BuscarConsultaPorId(int idConsulta);
        Task<ResponseModel<List<ConsultaModel>>> CriarConsulta(ConsultaCriacaoDto consultaCriacaoDto);
        Task<ResponseModel<List<ConsultaModel>>> EditarConsulta(ConsultaEdicaoDto consultaEdicaoDto);
        Task<ResponseModel<List<ConsultaModel>>> ExcluirConsulta(int idConsulta);
    }
}
