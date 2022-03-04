using ProjetoBlue.Entities;

namespace ProjetoBlue.Dto.Usuario
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public int Idade { get; set; }
        public string UserName { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
        public List<Entities.Agendamento> AgendamentosUser { get; set; }
    }
}
