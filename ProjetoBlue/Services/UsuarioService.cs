using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetoBlue.Dto.Usuario;
using ProjetoBlue.Entities;
using ProjetoBlue.Exceptions;
using ProjetoBlue.Helpers;
using BC = BCrypt.Net.BCrypt;

namespace ProjetoBlue.Services
{
    public interface IUsuarioService 
    {
        public Task<UsuarioResponse> Create(UsuarioRequest usuarioRequest);
        public Task<UsuarioResponse> GetById(int id);
        public Task<List<UsuarioResponse>> GetAll();
        public Task Update(UsuarioRequestUpdate usuarioRequest, int id);
        public Task Delete(int id);
    }
    public class UsuarioService : IUsuarioService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UsuarioService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UsuarioResponse> Create(UsuarioRequest usuarioRequest)
        {
            if (!usuarioRequest.Password.Equals(usuarioRequest.ConfirmPassword))  // conferi senha
                throw new BadRequestException("Senha não Conferi");               // exceção tratada
            Usuario usuarioDb = await _context.Usuarios
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserName == usuarioRequest.UserName); //verifica se existe um username igual

            if (usuarioDb is not null)  //verifica se é nulo
                throw new BadRequestException($"Usuario {usuarioRequest.UserName} já existe!"); //exceção tratada

            Usuario usuario = _mapper.Map<Usuario>(usuarioRequest); //tranformando em usuario

            /*if (usuarioRequest.AgendamentosUserIds.Any()) 
            {
                List<Agendamento> agendamentos = await _context.Agendamentos
                    .Where(a => usuarioRequest.AgendamentosUserIds.Contains(a.Id))
                    .ToListAsync();
                foreach (Agendamento agendamento in agendamentos)
                {
                    usuario.AgendamentosUser.Add(agendamento);
                }
            }*/

            DateTime now = DateTime.Now;
            usuario.CriadoEm = now;
            usuario.AtualizadoEm = now;

            usuario.Password = BC.HashPassword(usuario.Password);//faz o hash do password

            _context.Usuarios.Add(usuario);//adiciona o usuario

            await _context.SaveChangesAsync(); //salva a alteração

            return _mapper.Map<UsuarioResponse>(usuario);
        } 

        public async Task Delete(int id)
        {
            Usuario usuarioDb = await _context.Usuarios
                .SingleOrDefaultAsync(u=> u.Id == id);

            if (usuarioDb is null)
                throw new KeyNotFoundException($"Usuário {id} não encontrado");

            _context.Usuarios.Remove(usuarioDb);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UsuarioResponse>> GetAll()
        {
            List<Usuario> usuarios = await _context.Usuarios.ToListAsync();

            return usuarios.Select(u => _mapper.Map<UsuarioResponse>(u)).ToList();
        }

        public async Task<UsuarioResponse> GetById(int id)
        {
            Usuario usuarioDb = await _context.Usuarios //pega usuário
               .Include(u => u.AgendamentosUser) //traz todos os agendamento referentes ao usu[ario
               .SingleOrDefaultAsync(u => u.Id == id);

            if (usuarioDb is null)
                throw new KeyNotFoundException($"Usuário {id} não encontrado");

            return _mapper.Map<UsuarioResponse>(usuarioDb);
        }

        public async Task Update(UsuarioRequestUpdate usuarioRequest, int id)

        {
            if (usuarioRequest.Id != id)
            {
                throw new BadRequestException($"Id da Rota {id}, é diferente do id do Usuário");
            }
            else if (!usuarioRequest.Password.Equals(usuarioRequest.ConfirmPassword))
                throw new BadRequestException("Senha não confirmada");

            Usuario usuarioDb = await _context.Usuarios
                .Include(u =>u.AgendamentosUser)//não perco os dados caso tenha
                .SingleOrDefaultAsync(u => u.Id == id);

            if (usuarioDb is null)
            {
                throw new KeyNotFoundException($"Usuário {id} não encontrado");
            }
            else if (!BC.Verify(usuarioRequest.CurrentPassword, usuarioDb.Password))
            {
                throw new BadRequestException("Senha Incorreta");
            }

            DateTime now = DateTime.Now;
            usuarioDb.CriadoEm = usuarioDb.CriadoEm; //usuario permanece com sua data de criação.
            usuarioDb.AtualizadoEm = now; //só muda a atualização.

            await AddOrRemoveAgendamento(usuarioDb, usuarioRequest.AgendamentosUserIds); 

            usuarioDb = _mapper.Map<Usuario>(usuarioRequest);

            usuarioDb.Password = BC.HashPassword(usuarioRequest.Password); //faz o has password. 

            _context.Entry(usuarioDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();//atualiza.
        }
        private async Task AddOrRemoveAgendamento(Usuario usuariodb, int[] AgendamentosUserIds) //chamada de função que possibilita que usuário remova ou adicione agendamentos
        {
            int[] removeIds = usuariodb.AgendamentosUser.Where(a => 
                !AgendamentosUserIds.Contains(a.Id))
                .Select(a => a.Id).ToArray();//agendamentos temporarios para remover
            int[] AddedIds = AgendamentosUserIds
                .Where(a => !usuariodb.AgendamentosUser.Select(a => a.Id).ToArray().Contains(a))
                .ToArray();//agendamentos temporarios para adicionar.

            if (!removeIds.Any() && !AddedIds.Any()) //caso não tenha agendamento e nem passei nada.
            {
                _context.Entry(usuariodb).State = EntityState.Detached; //não restreia mais a entidade.
                return; //
            }

            List<Agendamento> todosAgendamentos = await _context.Agendamentos //caso tenha pego os adicionados e os removidos
                .Where(a => removeIds.Contains(a.Id) || AddedIds.Contains(a.Id))
                .ToListAsync();

            List<Agendamento> removeAgendamento = todosAgendamentos.Where(a => removeIds.Contains(a.Id)).ToList();
            foreach (Agendamento agendamento in removeAgendamento)
                usuariodb.AgendamentosUser.Remove(agendamento); //remove os temporários 

            List<Agendamento> adicionaAgendamento = todosAgendamentos.Where(a => AddedIds.Contains(a.Id)).ToList();
            foreach (Agendamento agendamento in adicionaAgendamento)
                usuariodb.AgendamentosUser.Add(agendamento); //adiciona os temporários

            await _context.SaveChangesAsync();//salva as alterações
            _context.Entry(usuariodb).State = EntityState.Detached;//deixa de rastrear.
        }
    }
}
