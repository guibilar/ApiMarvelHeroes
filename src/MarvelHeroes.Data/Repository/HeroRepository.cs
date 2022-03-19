using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models;
using MarvelHeroes.Data.Context;

namespace MarvelHeroes.Data.Repository
{
    public class HeroRepository : Repository<Hero>, IHeroRepository
    {
        public HeroRepository(MarvelHeroesDbContext context) : base(context) { }

    }
}
