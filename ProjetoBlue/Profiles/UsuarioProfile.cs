using AutoMapper;
using ProjetoBlue.Dto.Usuario;
using ProjetoBlue.Entities;

namespace ProjetoBlue.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioRequest>(); //transforma usuario em usuário request
            CreateMap<Usuario, UsuarioRequestUpdate>();
            CreateMap<Usuario, UsuarioResponse>();

            CreateMap<UsuarioRequest, Usuario>();//transformação inversa a de cima.
            CreateMap<UsuarioRequestUpdate, Usuario>();
            CreateMap<UsuarioResponse, Usuario>();

        }
    }
}
