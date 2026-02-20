using Microsoft.EntityFrameworkCore;
using WebApiClinicaOdonto.Data;
using WebApiClinicaOdonto.Dto.Dentista;
using WebApiClinicaOdonto.Models;

namespace WebApiClinicaOdonto.Services.Dentista
{
    public class DentistaService : IDentistaInterface
    {
        private readonly AppDbContext _context;

        public DentistaService(AppDbContext context)
        {
            _context = context;
        }
      
        public async Task<ResponseModel<DentistaModel>> BuscarDentistaPorId(int idDentista)
        {
            ResponseModel<DentistaModel> resposta = new ResponseModel<DentistaModel>();
            try
            {

                var dentista = await _context.Dentistas.FirstOrDefaultAsync(
                    dentistaBanco => dentistaBanco.Id == idDentista);

                if (dentista == null)
                {
                    resposta.Mensagem = "Dentista não encontrado.";
                    return resposta;
                }
                resposta.Dados = dentista;
                resposta.Mensagem = "Dentista encontrado com sucesso.";
                return resposta;

            }
            catch (Exception ex)
            {

                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<DentistaModel>>> CriarDentista(DentistaCriacaoDto dentistaCriacaoDto)
        {
            ResponseModel<List<DentistaModel>> resposta = new ResponseModel<List<DentistaModel>>();

            try
            {
                var dentista = new DentistaModel()
                {

                    Nome = dentistaCriacaoDto.Nome,
                    Especializacao = dentistaCriacaoDto.Especializacao,
                    Telefone = dentistaCriacaoDto.Telefone
                };

                _context.Add(dentista);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Dentistas.ToListAsync();
                resposta.Mensagem = "Dentista criado com sucesso.";

                return resposta;



            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<DentistaModel>>> EditarDentista(DentistaEdicaoDto dentistaEdicaoDto)
        {
            ResponseModel<List<DentistaModel>> resposta = new ResponseModel<List<DentistaModel>>();

            try
            {
               
                var dentista = await _context.Dentistas
                    .FirstOrDefaultAsync(dentistaBanco => dentistaBanco.Id == dentistaEdicaoDto.Id);

                if (dentista == null)
                {
                    resposta.Mensagem = "Dentista não encontrado.";
                    return resposta;
                }

                dentista.Nome = dentistaEdicaoDto.Nome;
                dentista.Especializacao = dentistaEdicaoDto.Especializacao;
                dentista.Telefone = dentistaEdicaoDto.Telefone;

                _context.Update(dentista);
                await _context.SaveChangesAsync();

                resposta.Dados = new List<DentistaModel> { dentista };
                resposta.Mensagem = "Dentista editado com sucesso.";
                return resposta;


            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }


        }

        public async Task<ResponseModel<List<DentistaModel>>> ExcluirDentista(int idDentista)
        {
            ResponseModel<List<DentistaModel>> resposta = new ResponseModel<List<DentistaModel>>();
            try
            {
                var dentista = await _context.Dentistas
                     .FirstOrDefaultAsync(dentistaBanco => dentistaBanco.Id == idDentista);

                if (dentista == null)
                {
                    resposta.Mensagem = "Dentista não encontrado.";
                    return resposta;
                }

                _context.Remove(dentista);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Dentistas.ToListAsync();
                resposta.Mensagem = "Dentista excluído com sucesso.";
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<DentistaModel>>> ListarDentistas()
        {
            ResponseModel<List<DentistaModel>> resposta = new ResponseModel<List<DentistaModel>>();
            try
            {
                var dentistas = await _context.Dentistas.ToListAsync();

                resposta.Dados = dentistas;
                resposta.Mensagem = "Dentistas listados com sucesso.";

                return resposta;


            }
            catch (Exception ex)
            {
                resposta.Mensagem=ex.Message;
                resposta.Status=false;
                return resposta;

            }
        }
    }
}
