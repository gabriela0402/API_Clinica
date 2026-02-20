using Microsoft.EntityFrameworkCore;
using WebApiClinicaOdonto.Models;

namespace WebApiClinicaOdonto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<PacienteModel> Pacientes { get; set; }
        public DbSet<DentistaModel> Dentistas { get; set; }
        public DbSet<ConsultaModel> Consultas { get; set; }
        public DbSet<ReceitaModel> Receitas { get; set; }

    }
}
