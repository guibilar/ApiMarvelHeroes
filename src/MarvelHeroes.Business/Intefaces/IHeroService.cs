using MarvelHeroes.Business.Models;
using System.Threading.Tasks;

namespace MarvelHeroes.Business.Intefaces
{
    public interface IHeroService
    {
        Task<bool> Add(Hero hero);
        Task<bool> Update(Hero hero);
    }
}