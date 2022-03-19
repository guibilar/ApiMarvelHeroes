namespace MarvelHeroes.Integração.Interfaces
{
    public interface IHeroIntegrationRepository
    {
        public dynamic ListHeros(int limit, int offset);

        public dynamic GetHero(int idMarvel);
    }
}
