using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiClinicaOdonto.Dto.Consulta;
using WebApiClinicaOdonto.Dto.Receita;
using WebApiClinicaOdonto.Models;
using WebApiClinicaOdonto.Services.Consulta;

namespace WebApiClinicaOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        private readonly IReceitaInterface _receitaInterface;
        public ReceitaController(IReceitaInterface receitaInterface)
        {
            _receitaInterface = receitaInterface;
        }

        [HttpGet("ListarReceitas")]
        public async Task<ActionResult<ResponseModel<List<ReceitaModel>>>> ListarReceitas()
        {
            var receitas = await _receitaInterface.ListarReceitas();
            return Ok(receitas);
        }

        [HttpGet("ListarReceitasPorPacienteId/{idPaciente}")]
        public async Task<ActionResult<ResponseModel<List<ReceitaModel>>>> ListarReceitasPorPacienteId(int idPaciente)
        {
            var receitas = await _receitaInterface.ListarReceitasPorPacienteId(idPaciente);
            return Ok(receitas);
        }

        [HttpGet("ListarReceitasPorDentistaId/{idDentista}")]
        public async Task<ActionResult<ResponseModel<List<ReceitaModel>>>> ListarReceitasPorDentistaId(int idDentista)
        {
            var receitas = await _receitaInterface.ListarReceitasPorDentistaId(idDentista);
            return Ok(receitas);
        }


        [HttpGet("BuscarReceitaPorId/{idReceita}")]
        public async Task<ActionResult<ResponseModel<ReceitaModel>>> BuscarReceitaPorId(int idReceita)
        {
            var receita = await _receitaInterface.BuscarReceitaPorId(idReceita);
            return Ok(receita);
        }

        [HttpPost("CriarReceita")]
        public async Task<ActionResult<ResponseModel<List<ReceitaModel>>>> CriarReceita(ReceitaCriacaoDto receitaCriacaoDto)
        {
            var receita = await _receitaInterface.CriarReceita(receitaCriacaoDto);
            return Ok(receita);
        }

        [HttpPut("EditarReceita")]
        public async Task<ActionResult<ResponseModel<List<ReceitaModel>>>> EditarReceita(ReceitaEdicaoDto receitaEdicaoDto)
        {
            var receita = await _receitaInterface.EditarReceita(receitaEdicaoDto);
            return Ok(receita);
        }

        [HttpDelete("ExcluirReceita/{idReceita}")]
        public async Task<ActionResult<ResponseModel<List<ReceitaModel>>>> ExcluirReceita(int idReceita)
        {
            var receita = await _receitaInterface.ExcluirReceita(idReceita);
            return Ok(receita);
        }


    }
}
