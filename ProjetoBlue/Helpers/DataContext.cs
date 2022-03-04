using Microsoft.EntityFrameworkCore;
using ProjetoBlue.Entities;

namespace ProjetoBlue.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<Agendamento>() //relacionamento das entidades
                .HasOne(u => u.Usuario) // agendamento tem um usuario
                .WithMany(u => u.AgendamentosUser) // usuario tem varios agentamentos
                .HasForeignKey(u => u.UsuarioId); // UsuarioId da classe Agendamento, representa chave estrangeira
                
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

    }
}
