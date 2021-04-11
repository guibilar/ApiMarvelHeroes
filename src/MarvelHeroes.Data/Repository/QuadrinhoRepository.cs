using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models;
using MarvelHeroes.Data.Context;

namespace MarvelHeroes.Data.Repository
{
    public class QuadrinhoRepository : Repository<Quadrinho>, IQuadrinhoRepository
    {
        public QuadrinhoRepository(MeuDbContext context) : base(context) { }

    }
}
