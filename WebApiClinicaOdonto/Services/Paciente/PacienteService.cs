using Microsoft.EntityFrameworkCore;
using WebApiClinicaOdonto.Data;
using WebApiClinicaOdonto.Dto.Dentista;
using WebApiClinicaOdonto.Dto.Paciente;
using WebApiClinicaOdonto.Models;

namespace WebApiClinicaOdonto.Services.Paciente
{
    public class PacienteService : IPacienteInterface
    {
        private readonly AppDbContext _context;

        public PacienteService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel<PacienteModel>> BuscarPacientePorId(int idPaciente)
        {
            ResponseModel<PacienteModel> resposta = new ResponseModel<PacienteModel>();
            try
            {

                var paciente = await _context.Pacientes.FirstOrDefaultAsync(
                    pacienteBanco => pacienteBanco.Id == idPaciente);

                if (paciente == null)
                {
                    resposta.Mensagem = "Paciente não encontrado.";
                    return resposta;
                }
                resposta.Dados = paciente;
                resposta.Mensagem = "Paciente encontrado com sucesso.";
                return resposta;

            }
            catch (Exception ex)
            {

                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<PacienteModel>>> CriarPaciente(PacienteCriacaoDto pacienteCriacaoDto)
        {
            ResponseModel<List<PacienteModel>> resposta = new ResponseModel<List<PacienteModel>>();

            try
            {
                var paciente = new PacienteModel()
                {

                    Nome = pacienteCriacaoDto.Nome,
                    CPF = pacienteCriacaoDto.CPF,
                    Telefone = pacienteCriacaoDto.Telefone
                };

                _context.Add(paciente);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Pacientes.ToListAsync();
                resposta.Mensagem = "Paciente criado com sucesso.";

                return resposta;



            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<PacienteModel>>> EditarPaciente(PacienteEdicaoDto pacienteEdicaoDto)
        {
            ResponseModel<List<PacienteModel>> resposta = new ResponseModel<List<PacienteModel>>();

            try
            {

                var paciente = await _context.Pacientes
                    .FirstOrDefaultAsync(pacienteBanco => pacienteBanco.Id == pacienteEdicaoDto.Id);

                if (paciente == null)
                {
                    resposta.Mensagem = "Paciente não encontrado.";
                    return resposta;
                }

                paciente.Id = pacienteEdicaoDto.Id;
                paciente.Nome = pacienteEdicaoDto.Nome;
                paciente.CPF = pacienteEdicaoDto.CPF;
                paciente.Telefone = pacienteEdicaoDto.Telefone;

                _context.Update(paciente);
                await _context.SaveChangesAsync();

                resposta.Dados = new List<PacienteModel> { paciente };
                resposta.Mensagem = "Paciente editado com sucesso.";
                return resposta;


            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<PacienteModel>>> ExcluirPaciente(int idPaciente)
        {
            ResponseModel<List<PacienteModel>> resposta = new ResponseModel<List<PacienteModel>>();
            try
            {
                var paciente = await _context.Pacientes
                     .FirstOrDefaultAsync(pacienteBanco => pacienteBanco.Id == idPaciente);

                if (paciente == null)
                {
                    resposta.Mensagem = "Paciente não encontrado.";
                    return resposta;
                }

                _context.Remove(paciente);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Pacientes.ToListAsync();
                resposta.Mensagem = "Paciente excluído com sucesso.";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<PacienteModel>>> ListarPacientes()
        {
            ResponseModel<List<PacienteModel>> resposta = new ResponseModel<List<PacienteModel>>();
            try
            {
                var pacientes = await _context.Pacientes.ToListAsync();

                resposta.Dados = pacientes;
                resposta.Mensagem = "Pacientes listados com sucesso.";

                return resposta;


            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }
    }

}
