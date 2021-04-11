using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models;
using MarvelHeroes.Data.Context;

namespace MarvelHeroes.Data.Repository
{
    public class PersonagemRepository : Repository<Personagem>, IPersonagemRepository
    {
        public PersonagemRepository(MeuDbContext context) : base(context) { }

    }
}
