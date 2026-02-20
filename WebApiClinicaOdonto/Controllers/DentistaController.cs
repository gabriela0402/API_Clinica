using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiClinicaOdonto.Dto.Dentista;
using WebApiClinicaOdonto.Models;
using WebApiClinicaOdonto.Services.Dentista;

namespace WebApiClinicaOdonto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentistaController : ControllerBase
    {
        private readonly IDentistaInterface _dentistaInterface;

        public DentistaController(IDentistaInterface dentistaInterface)
        {
            _dentistaInterface = dentistaInterface;
        }

        [HttpGet("ListarDentistas")]
        public async Task<ActionResult<ResponseModel<List<DentistaModel>>>> ListarDentistas()
        {
            var dentistas = await _dentistaInterface.ListarDentistas();
            return Ok(dentistas);
        }

        [HttpGet("BuscarDentistaPorId/{idDentista}")]
        public async Task<ActionResult<ResponseModel<DentistaModel>>> BuscarDentistaPorId(int idDentista)
        {
            var dentista = await _dentistaInterface.BuscarDentistaPorId(idDentista);

            return Ok(dentista);
        }

        [HttpPost("CriarDentista")]

        public async Task<ActionResult<ResponseModel<List<DentistaModel>>>> CriarDentista(DentistaCriacaoDto dentistaCriacaoDto)
        {
            var dentistas = await _dentistaInterface.CriarDentista(dentistaCriacaoDto);

            return Ok(dentistas);
        }

        [HttpPut("EditarDentista")]

        public async Task<ActionResult<ResponseModel<List<DentistaModel>>>> EditarDentista( DentistaEdicaoDto dentistaEdicaoDto)
        {
            var dentista = await _dentistaInterface.EditarDentista(dentistaEdicaoDto);

            return Ok(dentista);
        }

        [HttpDelete("ExcluirDentista")]
        public async Task<ActionResult<ResponseModel<List<DentistaModel>>>> ExcluirDentista(int idDentista)
        {
            var dentista = await _dentistaInterface.ExcluirDentista(idDentista);

            return Ok(dentista);
        }
    }
}
