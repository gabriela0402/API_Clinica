using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiClinicaOdonto.Dto.Consulta;
using WebApiClinicaOdonto.Models;
using WebApiClinicaOdonto.Services.Consulta;
using WebApiClinicaOdonto.Services.Dentista;

namespace WebApiClinicaOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaInterface _consultaInterface;
        public ConsultaController(IConsultaInterface consultaInterface)
        {
            _consultaInterface = consultaInterface;
        }

        [HttpGet("ListarConsultas")]
        public async Task<ActionResult<ResponseModel<List<ConsultaModel>>>> ListarConsultas()
        {
            var consultas = await _consultaInterface.ListarConsultas();
            return Ok(consultas);
        }

        [HttpGet("ListarConsultasPorPacienteId/{idPaciente}")] 
        public async Task<ActionResult<ResponseModel<List<ConsultaModel>>>> ListarConsultasPorPacienteId(int idPaciente)
        {
            var consulta = await _consultaInterface.ListarConsultasPorPacienteId(idPaciente);
            return Ok(consulta);
        }

        [HttpGet("ListarConsultasPorDentistaId/{idDentista}")]
        public async Task<ActionResult<ResponseModel<List<ConsultaModel>>>> ListarConsultasPorDentistaId(int idDentista)
        {
            var consulta = await _consultaInterface.ListarConsultasPorDentistaId(idDentista);
            return Ok(consulta);
        }

        [HttpGet("BuscarConsultaPorId")]
        public async Task<ActionResult<ResponseModel<List<ConsultaModel>>>> BuscarConsultaPorId(int idConsulta)
        {
            var consulta = await _consultaInterface.BuscarConsultaPorId(idConsulta);
            return Ok(consulta);
        }

        [HttpPost("CriarConsulta")]
        public async Task<ActionResult<ResponseModel<List<ConsultaModel>>>> CriarConsulta(ConsultaCriacaoDto consultaCriacaoDto)
        {
            var consulta = await _consultaInterface.CriarConsulta(consultaCriacaoDto);
            return Ok(consulta);
        }

        [HttpPut("EditarConsulta")]
        public async Task<ActionResult<ResponseModel<List<ConsultaModel>>>> EditarConsulta(ConsultaEdicaoDto consultaEdicaoDto)
        {
            var consulta = await _consultaInterface.EditarConsulta(consultaEdicaoDto);
            return Ok(consulta);
        }

        [HttpDelete("ExcluirConsulta")]
        public async Task<ActionResult<ResponseModel<List<ConsultaModel>>>> ExcluirConsulta(int idConsulta)
        {
            var consulta = await _consultaInterface.ExcluirConsulta(idConsulta);
            return Ok(consulta);
        }
    }
}
