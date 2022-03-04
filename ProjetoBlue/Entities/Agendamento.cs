using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoBlue.Entities
{
    public class Agendamento : BaseEntidades // classe criada para mostrar trabalho com relações.
    {
        [ForeignKey("Usuario")]
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Email { get; set; }
        public string telefone { get; set; }
        public int UsuarioId { get; set; }
        [JsonIgnore]//evita looping
        public virtual Usuario Usuario { get; set; }
    }
}
