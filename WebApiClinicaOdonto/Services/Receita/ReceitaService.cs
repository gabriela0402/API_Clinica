using Microsoft.EntityFrameworkCore;
using WebApiClinicaOdonto.Data;
using WebApiClinicaOdonto.Dto.Consulta;
using WebApiClinicaOdonto.Dto.Receita;
using WebApiClinicaOdonto.Models;

namespace WebApiClinicaOdonto.Services.Consulta
{
    public class ReceitaService : IReceitaInterface
    {
        private readonly AppDbContext _context;

        public ReceitaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<ReceitaModel>> BuscarReceitaPorId(int idReceita)
        {
            ResponseModel<ReceitaModel> resposta = new ResponseModel<ReceitaModel>();
            try
            {

                var receita = await _context.Receitas
                    .Include(c => c.Consulta)
                    .FirstOrDefaultAsync(
                    receitaBanco => receitaBanco.Id == idReceita);

                if (receita == null)
                {
                    resposta.Mensagem = "Receita não encontrada.";
                    return resposta;
                }

                resposta.Dados = receita;
                resposta.Mensagem = "Receita encontrada com sucesso.";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ReceitaModel>>> CriarReceita(ReceitaCriacaoDto receitaCriacaoDto)
        {
            ResponseModel<List<ReceitaModel>> resposta = new ResponseModel<List<ReceitaModel>>();

            try
            {
                var consultaeExiste = await _context.Consultas.AnyAsync(p => p.Id == receitaCriacaoDto.ConsultaId);
                if (!consultaeExiste)
                {
                    resposta.Mensagem = "Consulta não encontrada";
                    resposta.Status = false;
                    return resposta;
                }

                var receita = new ReceitaModel()
                {
                    ConsultaId = receitaCriacaoDto.ConsultaId,
                    Prescricao = receitaCriacaoDto.Prescricao,
                    Remedio = receitaCriacaoDto.Remedio
                };

                _context.Add(receita);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Receitas
                    .Include(c => c.Consulta)
                    .ToListAsync();

                resposta.Mensagem = "Receita criada com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ReceitaModel>>> EditarReceita(ReceitaEdicaoDto receitaEdicaoDto)
        {
            ResponseModel<List<ReceitaModel>> resposta = new ResponseModel<List<ReceitaModel>>();

            try
            {
                var consultaExiste = await _context.Consultas.AnyAsync(c => c.Id == receitaEdicaoDto.ConsultaId);
                if (!consultaExiste)
                {
                    resposta.Mensagem = "A consulta não existe. Não é possível editar a receita.";
                    resposta.Status = false;
                    return resposta;
                }

                var receita = await _context.Receitas
                    .Include(c => c.Consulta)
                    .FirstOrDefaultAsync(receitaBanco => receitaBanco.Id == receitaEdicaoDto.Id);

                if (receita == null)
                {
                    resposta.Mensagem = "Receita não encontrada.";
                    return resposta;
                }

                receita.ConsultaId = receitaEdicaoDto.ConsultaId;
                receita.Prescricao = receitaEdicaoDto.Prescricao;
                receita.Remedio = receitaEdicaoDto.Remedio;

                _context.Update(receita);
                await _context.SaveChangesAsync();

                resposta.Dados = new List<ReceitaModel> { receita };
                resposta.Mensagem = "Receita editada com sucesso.";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ReceitaModel>>> ExcluirReceita(int idReceita)
        {
            ResponseModel<List<ReceitaModel>> resposta = new ResponseModel<List<ReceitaModel>>();
            try
            {
                var receita = await _context.Receitas
                     .FirstOrDefaultAsync(receitaBanco => receitaBanco.Id == idReceita);

                if (receita == null)
                {
                    resposta.Mensagem = "Reecita não encontrada.";
                    return resposta;
                }

                _context.Remove(receita);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Receitas.ToListAsync();
                resposta.Mensagem = "Receita excluída com sucesso.";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<ReceitaModel>>> ListarReceitas()
        {
            ResponseModel<List<ReceitaModel>> resposta = new ResponseModel<List<ReceitaModel>>();
            try
            {
                resposta.Dados = await _context.Receitas
                    .Include(c => c.Consulta)
                    .ToListAsync();
                resposta.Mensagem = "Receitas listadas com sucesso.";

                return resposta;


            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<List<ReceitaModel>>> ListarReceitasPorDentistaId(int idDentista)
        {
            ResponseModel<List<ReceitaModel>> resposta = new ResponseModel<List<ReceitaModel>>();
            try
            {
                var dentistaExiste = await _context.Dentistas.AnyAsync(p => p.Id == idDentista);

                if (!dentistaExiste)
                {
                    resposta.Mensagem = "Dentista não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }


                var receitas = await _context.Receitas
                    .Include(c => c.Consulta)
                    .Where(c => c.Consulta.DentistaId == idDentista)
                    .ToListAsync();

                if (receitas.Count == 0)
                {
                    resposta.Mensagem = "Nenhuma receita encontrada para este dentista.";
                    return resposta;
                }


                resposta.Dados = receitas;
                resposta.Mensagem = "Receitas listados com sucesso.";
                return resposta;


            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<List<ReceitaModel>>> ListarReceitasPorPacienteId(int idPaciente)
        {
            ResponseModel<List<ReceitaModel>> resposta = new ResponseModel<List<ReceitaModel>>();
            try
            {
                var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == idPaciente);

                if (!pacienteExiste)
                {
                    resposta.Mensagem = "Paciente não encontrado.";
                    resposta.Status = false;
                    return resposta;
                }

                var receitas = await _context.Receitas
                    .Include(c => c.Consulta)
                    .Where(c => c.Consulta.PacienteId == idPaciente)
                    .ToListAsync();

                if (receitas.Count == 0)
                {
                    resposta.Mensagem = "Nenhuma receita encontrada para este paciente.";
                    return resposta;
                }

                resposta.Dados = receitas;
                resposta.Mensagem = "Receitas listados com sucesso.";
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
