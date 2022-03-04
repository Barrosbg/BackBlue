using AutoMapper;
using ProjetoBlue.Dto.Agendamento;
using ProjetoBlue.Entities;

namespace ProjetoBlue.Profiles
{
    public class AgendamentoProfile : Profile
    {
        public AgendamentoProfile()
        {
            CreateMap<Agendamento, AgendamentoRequest>();
            CreateMap<Agendamento, AgendamentoResponse>();

            CreateMap<AgendamentoRequest, Agendamento>();
            CreateMap<AgendamentoResponse, Agendamento>();
        }
    }
}
