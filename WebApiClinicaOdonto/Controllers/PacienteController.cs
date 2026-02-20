using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiClinicaOdonto.Dto.Dentista;
using WebApiClinicaOdonto.Dto.Paciente;
using WebApiClinicaOdonto.Models;
using WebApiClinicaOdonto.Services.Dentista;
using WebApiClinicaOdonto.Services.Paciente;

namespace WebApiClinicaOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteInterface _pacienteInterface;

        public PacienteController(IPacienteInterface pacienteInterface)
        {
            _pacienteInterface = pacienteInterface;
        }

        [HttpGet("ListarPacientes")]
        public async Task<ActionResult<ResponseModel<List<PacienteModel>>>> ListarPacientes()
        {
            var pacientes = await _pacienteInterface.ListarPacientes();
            return Ok(pacientes);
        }

        [HttpGet("BuscarPacientePorId/{idPaciente}")]
        public async Task<ActionResult<ResponseModel<PacienteModel>>> BuscarPacientePorId(int idPaciente)
        {
            var paciente = await _pacienteInterface.BuscarPacientePorId(idPaciente);

            return Ok(paciente);
        }

        [HttpPost("CriarPaciente")]
        public async Task<ActionResult<ResponseModel<PacienteModel>>> CriarPaciente(PacienteCriacaoDto pacienteCriacaoDto)
        {
            var pacientes = await _pacienteInterface.CriarPaciente(pacienteCriacaoDto);

            return Ok(pacientes);
        } 


        [HttpPut("EditarPaciente")]
        public async Task<ActionResult<ResponseModel<List<PacienteModel>>>> EditarPaciente(PacienteEdicaoDto pacienteEdicaoDto)
        {
            var paciente = await _pacienteInterface.EditarPaciente(pacienteEdicaoDto);

            return Ok(paciente);
        }

        [HttpDelete("ExcluirPaciente")]

        public async Task<ActionResult<ResponseModel<List<PacienteModel>>>> ExcluirPaciente(int idPaciente)
        {
            var paciente = await _pacienteInterface.ExcluirPaciente(idPaciente);

            return Ok(paciente);
        }

    }
}
