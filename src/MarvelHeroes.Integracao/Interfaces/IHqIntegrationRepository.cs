namespace MarvelHeroes.Integração.Interfaces
{
    public interface IHqIntegrationRepository
    {
        dynamic GetHeroHq(int idMarvelHero, int limit, int offset);
        dynamic GetHq(int idMarvel);
    }
}