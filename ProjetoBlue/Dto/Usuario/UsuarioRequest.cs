namespace ProjetoBlue.Dto.Usuario
{
    public class UsuarioRequest
    {
        public int Id { get; set; }    
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public int Idade { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int[] AgendamentosUserIds { get; set; }
    }
}
