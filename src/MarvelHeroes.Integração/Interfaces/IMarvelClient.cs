
namespace MarvelHeroes.Integração.Interfaces
{
    public interface IMarvelClient
    {
        public dynamic ObterListagem(string caminho, int limite, int offset);

        public dynamic ObterObjeto(string caminho, int idMarvel);
    }
}
