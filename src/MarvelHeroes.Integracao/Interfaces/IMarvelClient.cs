
namespace MarvelHeroes.Integração.Interfaces
{
    public interface IMarvelClient
    {
        public dynamic GetList(string caminho, int limite, int offset);

        public dynamic GetObject(string caminho, int idMarvel);
    }
}
