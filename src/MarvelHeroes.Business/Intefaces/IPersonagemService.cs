using MarvelHeroes.Business.Models;
using System.Threading.Tasks;

namespace MarvelHeroes.Business.Intefaces
{
    public interface IPersonagemService
    {
        Task<bool> Adicionar(Personagem personagem);
        Task<bool> Atualizar(Personagem personagem);
    }
}