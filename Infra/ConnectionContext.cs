using Microsoft.EntityFrameworkCore;
using PIM_IV.Models;

namespace PIM_IV.Infra
{
    public class ConnectionContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "User Id=postgres;Password=4jPbxq29N02eT66c;Server=db.slyuvvpzwhypsrvpuojy.supabase.co;Port=5432;Database=postgres";

                optionsBuilder.UseNpgsql(connectionString);
            }
        }
        public DbSet<PIM_IV.Models.FuncionarioModel> FuncionarioModel { get; set; } = default!;
        public DbSet<PIM_IV.Models.ContatoModel> ContatoModel { get; set; } = default!;
        public DbSet<PIM_IV.Models.EnderecoModel> EnderecoModel { get; set; } = default!;
        public DbSet<PIM_IV.Models.DependentesModel> DependentesModel { get; set; } = default!;
        public DbSet<PIM_IV.Models.RecursosHumanosModel> RecursosHumanosModel { get; set; } = default!;
        public DbSet<PIM_IV.Models.RegistroPontoModel> RegistroPontoModel { get; set; } = default!;
        public DbSet<PIM_IV.Models.FolhaDePagamentoModel> FolhaDePagamentoModel { get; set; } = default!;
    }
}