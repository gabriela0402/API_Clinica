using Microsoft.EntityFrameworkCore;
using WebApiClinicaOdonto.Data;
using WebApiClinicaOdonto.Dto.Consulta;
using WebApiClinicaOdonto.Dto.Dentista;
using WebApiClinicaOdonto.Models;

namespace WebApiClinicaOdonto.Services.Consulta
{
    public class ConsultaService : IConsultaInterface
    {
        private readonly AppDbContext _context;

        public ConsultaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<ConsultaModel>> BuscarConsultaPorId(int idConsulta)
        {
            ResponseModel<ConsultaModel> resposta = new ResponseModel<ConsultaModel>();
            try
            {

                var consulta = await _context.Consultas
                    .Include(c => c.Paciente)
                    .Include(c => c.Dentista)
                    .Include(c => c.Receitas)
                    .FirstOrDefaultAsync(
                    consultabanco => consultabanco.Id == idConsulta);

                if (consulta == null)
                {
                    resposta.Mensagem = "Consulta não encontrada.";
                    return resposta;
                }

                resposta.Dados = consulta;
                resposta.Mensagem = "Consulta encontrado com sucesso.";
                return resposta;

            }
            catch (Exception ex)
            { 
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ConsultaModel>>> CriarConsulta(ConsultaCriacaoDto consultaCriacaoDto)
        {
            ResponseModel<List<ConsultaModel>> resposta = new ResponseModel<List<ConsultaModel>>();

            try
            {
                if (consultaCriacaoDto.Data_Horario < DateTime.Now)
                {
                    resposta.Mensagem = "A data e horário da consulta não pode ser no passado.";
                    resposta.Status = false;
                    return resposta;
                }

                var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == consultaCriacaoDto.PacienteId);
                if (!pacienteExiste)
                {
                    resposta.Mensagem = "Paciente não encontrado";
                    resposta.Status = false;
                    return resposta;
                }

                var dentistaExiste = await _context.Dentistas.AnyAsync(d => d.Id == consultaCriacaoDto.DentistaId);
                if (!dentistaExiste)
                {
                    resposta.Mensagem = "Dentista não encontrado";
                    resposta.Status = false;
                    return resposta;
                }

                var consulta = new ConsultaModel()
                {
                    PacienteId = consultaCriacaoDto.PacienteId,
                    DentistaId = consultaCriacaoDto.DentistaId,
                    Data_Horario = consultaCriacaoDto.Data_Horario,
                    Sintoma = consultaCriacaoDto.Sintoma,
                    Tratamento = consultaCriacaoDto.Tratamento,
                    Procedimento = consultaCriacaoDto.Procedimento
                };

                _context.Add(consulta);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Consultas
                    .Include(c => c.Paciente)
                    .Include(c => c.Dentista)
                    .Include(c => c.Receitas)
                    .ToListAsync();

                resposta.Mensagem = "Consulta criada com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ConsultaModel>>> EditarConsulta(ConsultaEdicaoDto consultaEdicaoDto)
        {
            ResponseModel<List<ConsultaModel>> resposta = new ResponseModel<List<ConsultaModel>>();

            try
            {
                var consulta = await _context.Consultas
                    .Include(c => c.Paciente)
                    .Include(c => c.Dentista)
                    .Include(c => c.Receitas)
                    .FirstOrDefaultAsync(consultaBanco => consultaBanco.Id == consultaEdicaoDto.Id);

                if (consulta == null)
                {
                    resposta.Mensagem = "Consulta não encontrada.";
                    return resposta;
                }

                consulta.PacienteId = consultaEdicaoDto.PacienteId;
                consulta.DentistaId = consultaEdicaoDto.DentistaId;
                consulta.Data_Horario = consultaEdicaoDto.Data_Horario;
                consulta.Sintoma = consultaEdicaoDto.Sintoma;
                consulta.Tratamento = consultaEdicaoDto.Tratamento;
                consulta.Procedimento = consultaEdicaoDto.Procedimento;

                _context.Update(consulta);
                await _context.SaveChangesAsync();

                resposta.Dados = new List<ConsultaModel> { consulta};
                resposta.Mensagem = "Consulta editada com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }

        }

        public async Task<ResponseModel<List<ConsultaModel>>> ExcluirConsulta(int idConsulta)
        {
            ResponseModel<List<ConsultaModel>> resposta = new ResponseModel<List<ConsultaModel>>();
            try
            {
                var consulta = await _context.Consultas
                     .FirstOrDefaultAsync(consultaBanco => consultaBanco.Id == idConsulta);

                if (consulta == null)
                {
                    resposta.Mensagem = "Consulta não encontrada.";
                    return resposta;
                }

                _context.Remove(consulta);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Consultas.ToListAsync();
                resposta.Mensagem = "Consulta excluída com sucesso.";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ConsultaModel>>> ListarConsultas()
        {
            ResponseModel<List<ConsultaModel>> resposta = new ResponseModel<List<ConsultaModel>>();
            try
            {
                resposta.Dados = await _context.Consultas
                    .Include(c => c.Paciente)
                    .Include(c => c.Dentista)
                    .Include(c => c.Receitas)
                    .ToListAsync();
                resposta.Mensagem = "Consultas listados com sucesso.";

                return resposta;


            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<List<ConsultaModel>>> ListarConsultasPorDentistaId(int idDentista)
        {
            ResponseModel<List<ConsultaModel>> resposta = new ResponseModel<List<ConsultaModel>>();
            try
            {
                var dentistaExiste = await _context.Pacientes.AnyAsync(p => p.Id == idDentista);

                if (!dentistaExiste)
                {
                    resposta.Mensagem = "Dentista não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }

                var consultas = await _context.Consultas
                    .Include(c => c.Paciente)
                    .Include(c => c.Dentista)
                    .Include(c => c.Receitas)
                    .Where(c => c.PacienteId == idDentista)
                    .ToListAsync();

                if (consultas.Count == 0)
                {
                    resposta.Mensagem = "Nenhuma consulta encontrada para este dentista.";
                    return resposta;
                }


                resposta.Dados = consultas;
                resposta.Mensagem = "Consultas listados com sucesso.";
                return resposta;


            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<List<ConsultaModel>>> ListarConsultasPorPacienteId(int idPaciente)
        {
            ResponseModel<List<ConsultaModel>> resposta = new ResponseModel<List<ConsultaModel>>();
            try
            {
                var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == idPaciente);

                if (!pacienteExiste)
                {
                    resposta.Mensagem = "Paciente não encontrado.";
                    resposta.Status = false; 
                    return resposta;
                }


                var consultas = await _context.Consultas
                    .Include(c => c.Paciente)
                    .Include(c => c.Dentista)
                    .Include(c => c.Receitas)
                    .Where(c => c.PacienteId == idPaciente)
                    .ToListAsync();



                if (consultas.Count == 0)
                {
                   resposta.Mensagem = "Nenhuma consulta encontrada para este paciente.";
                   return resposta;
                }

                resposta.Dados = consultas;
                resposta.Mensagem = "Consultas listados com sucesso.";
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
