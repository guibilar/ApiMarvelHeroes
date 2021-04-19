using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models;
using MarvelHeroes.Business.Models.Validations;
using System.Linq;
using System.Threading.Tasks;

namespace MarvelHeroes.Business.Services
{
    public class PersonagemService : BaseService, IPersonagemService
    {
        private readonly IPersonagemRepository _personagemRepository;

        public PersonagemService(IPersonagemRepository personagemRepository,
                                 INotificador notificador) : base(notificador)
        {
            _personagemRepository = personagemRepository;
        }

        public async Task<bool> Adicionar(Personagem personagem)
        {
            if (!ExecutarValidacao(new PersonagemValidation(), personagem)) return false;

            if (_personagemRepository.Buscar(f => f.IdMarvel == personagem.IdMarvel).Result.Any())
            {
                Notificar(Models.Enums.TipoNotificacao.Aviso, "Já existe um personagem cadastrado com esse IdMarvel.");
                return false;
            }

            await _personagemRepository.Adicionar(personagem);
            return true;
        }

        public async Task<bool> Atualizar(Personagem personagem)
        {
            var personagemEmBanco = await _personagemRepository.ObterPorGuid(personagem.Guid);
            personagem.Id = personagemEmBanco.Id;

            if (!ExecutarValidacao(new PersonagemValidation(), personagem)) return false;

            if (_personagemRepository.Buscar(f => f.Id == personagem.Id && f.IdMarvel != personagem.IdMarvel).Result.Any())
            {
                Notificar(Models.Enums.TipoNotificacao.Aviso, "Já existe um personagem cadastrado com esse IdMarvel.");
                return false;
            }

            if(personagem.IdMarvel != personagemEmBanco.IdMarvel)
            {
                Notificar(Models.Enums.TipoNotificacao.Aviso, "Não é possível alterar um IdMarvel.");
                return false;
            }

            await _personagemRepository.Atualizar(personagem);
            return true;
        }

        public void Dispose()
        {
            _personagemRepository?.Dispose();
        }
    }
}
