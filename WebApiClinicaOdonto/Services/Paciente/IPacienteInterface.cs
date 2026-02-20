using WebApiClinicaOdonto.Dto.Dentista;
using WebApiClinicaOdonto.Dto.Paciente;
using WebApiClinicaOdonto.Models;

namespace WebApiClinicaOdonto.Services.Paciente
{
    public interface IPacienteInterface
    {
        Task<ResponseModel<List<PacienteModel>>> ListarPacientes();
        Task<ResponseModel<PacienteModel>> BuscarPacientePorId(int idPaciente);
        Task<ResponseModel<List<PacienteModel>>> CriarPaciente(PacienteCriacaoDto paciente);
        Task<ResponseModel<List<PacienteModel>>> EditarPaciente(PacienteEdicaoDto pacienteEdicaoDto);
        Task<ResponseModel<List<PacienteModel>>> ExcluirPaciente(int idPaciente);
    }
}
