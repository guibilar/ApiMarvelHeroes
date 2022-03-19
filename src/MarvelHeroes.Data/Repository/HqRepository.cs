using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models;
using MarvelHeroes.Data.Context;

namespace MarvelHeroes.Data.Repository
{
    public class HqRepository : Repository<Hq>, IHqRepository
    {
        public HqRepository(MarvelHeroesDbContext context) : base(context) { }

    }
}
