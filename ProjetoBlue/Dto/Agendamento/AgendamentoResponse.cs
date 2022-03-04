namespace ProjetoBlue.Dto.Agendamento
{
    public class AgendamentoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Email { get; set; }
        public string telefone { get; set; }
        public int UsuarioId { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
        public virtual Entities.Usuario Usuario { get; set; }
    }
}
