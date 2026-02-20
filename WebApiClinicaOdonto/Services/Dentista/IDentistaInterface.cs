using WebApiClinicaOdonto.Dto.Dentista;
using WebApiClinicaOdonto.Models;

namespace WebApiClinicaOdonto.Services.Dentista
{
    public interface IDentistaInterface
    {
        Task<ResponseModel<List<DentistaModel>>> ListarDentistas();
        Task<ResponseModel<DentistaModel>> BuscarDentistaPorId(int idDentista);
        Task<ResponseModel<List<DentistaModel>>> CriarDentista(DentistaCriacaoDto dentista);
        Task<ResponseModel<List<DentistaModel>>> EditarDentista(DentistaEdicaoDto dentistaEdicaoDto);
        Task<ResponseModel<List<DentistaModel>>> ExcluirDentista(int idDentista);

    }
}
