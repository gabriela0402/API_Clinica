using WebApiClinicaOdonto.Dto.Consulta;
using WebApiClinicaOdonto.Dto.Receita;
using WebApiClinicaOdonto.Models;

namespace WebApiClinicaOdonto.Services.Consulta
{
    public interface IReceitaInterface
    {
        Task<ResponseModel<List<ReceitaModel>>> ListarReceitas();
        Task<ResponseModel<List<ReceitaModel>>> ListarReceitasPorPacienteId(int idPaciente);
        Task<ResponseModel<List<ReceitaModel>>> ListarReceitasPorDentistaId(int idDentista);
        Task<ResponseModel<ReceitaModel>> BuscarReceitaPorId(int idReceita);
        Task<ResponseModel<List<ReceitaModel>>> CriarReceita( ReceitaCriacaoDto receitaCriacaoDto);
        Task<ResponseModel<List<ReceitaModel>>> EditarReceita(ReceitaEdicaoDto receitaEdicaoDto);
        Task<ResponseModel<List<ReceitaModel>>> ExcluirReceita(int idReceita);

    }
}
