using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoBlue.Entities
{
    public class Usuario : BaseEntidades
    {
        public string Nome { get; set; }
        public string SobreNome { get; set;}
        public int Idade { get; set;}
        public string UserName { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }
        [NotMapped]
        public string CurrentPassword { get; set; } 
        [JsonIgnore]//evita looping
        public ICollection<Agendamento> AgendamentosUser { get; set; }

    }
}
