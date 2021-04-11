namespace MarvelHeroes.Integração.Interfaces
{
    public interface IQuadrinhoIntegracaoRepository
    {
        dynamic ListaQuadrinhosDePersonagem(int idMarvelPersonagem, int limite, int offset);
        dynamic ObterQuadrinho(int idMarvel);
    }
}