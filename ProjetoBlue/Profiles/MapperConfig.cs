using AutoMapper;

namespace ProjetoBlue.Profiles
{
    public static class MapperConfig
    {
        public static MapperConfiguration GetMapperConfig()
        {
            return new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UsuarioProfile());
                mc.AddProfile(new AgendamentoProfile());
            });
        }
    }
}
