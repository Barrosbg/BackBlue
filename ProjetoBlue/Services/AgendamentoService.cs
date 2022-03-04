using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetoBlue.Dto.Agendamento;
using ProjetoBlue.Entities;
using ProjetoBlue.Exceptions;
using ProjetoBlue.Helpers;

namespace ProjetoBlue.Services
{
    public interface IAgendamentoService
    {
        public Task<AgendamentoResponse> Create(AgendamentoRequest agendamento);
        public Task<AgendamentoResponse> GetById(int id);
        public Task<List<AgendamentoResponse>> GetAll();
        public Task Update(AgendamentoRequest AgendamentoIn, int id);
        public Task Delete(int id);
    }
    public class AgendamentoService : IAgendamentoService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AgendamentoService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AgendamentoResponse> Create(AgendamentoRequest agendamentoRequest)
        {
            Agendamento agendamento = _mapper.Map<Agendamento>(agendamentoRequest);

            DateTime now = DateTime.Now;
            agendamento.CriadoEm = now;
            agendamento.AtualizadoEm = now;

            _context.Agendamentos.Add(agendamento);

            await _context.SaveChangesAsync();

            return _mapper.Map<AgendamentoResponse>(agendamento);
        }

        public async Task Delete(int id)
        {
            Agendamento AgendamentoDb = await _context.Agendamentos
                .SingleOrDefaultAsync(a => a.Id == id);

            if (AgendamentoDb is null)
                throw new KeyNotFoundException($"Agendamento {id} não encontrado");

            _context.Agendamentos.Remove(AgendamentoDb);
            await _context.SaveChangesAsync();
        }
        public async Task<List<AgendamentoResponse>> GetAll()
        {
            List<Agendamento> agendamento = await _context.Agendamentos.ToListAsync();
            return agendamento.Select(a => _mapper.Map<AgendamentoResponse>(a)).ToList();
        }

        public async Task<AgendamentoResponse> GetById(int id)
        {
            Agendamento AgendamentoDb = await _context.Agendamentos
                //.Include(u => u.Usuario)////*****
               .SingleOrDefaultAsync(a => a.Id == id);

            if (AgendamentoDb is null)
                throw new KeyNotFoundException($"Agendamento {id} não encontrado");

            return _mapper.Map<AgendamentoResponse>(AgendamentoDb);
        }

        public async Task Update(AgendamentoRequest AgendamentoRequest, int id)
        {
            if (AgendamentoRequest.Id != id)
            {
                throw new BadRequestException($"Id da Rota {id}, é diferente do id do Agendamento");
            }

            Agendamento AgendamentoDb = await _context.Agendamentos
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id);

            if (AgendamentoDb is null)
            {
                throw new BadRequestException($"Agendamento {id} não encontrado");
            }

            AgendamentoDb = _mapper.Map<Agendamento>(AgendamentoRequest);

            DateTime now = DateTime.Now;
            AgendamentoDb.CriadoEm = AgendamentoDb.CriadoEm;
            AgendamentoDb.AtualizadoEm = now;

            _context.Entry(AgendamentoDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
