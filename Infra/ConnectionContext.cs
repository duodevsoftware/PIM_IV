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
    }
}