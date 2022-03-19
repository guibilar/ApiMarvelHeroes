using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models;
using MarvelHeroes.Business.Models.Validations;
using System.Linq;
using System.Threading.Tasks;

namespace MarvelHeroes.Business.Services
{
    public class HeroService : BaseService, IHeroService
    {
        private readonly IHeroRepository _heroRepository;

        public HeroService(IHeroRepository heroRepository,
                                 INotificator notificator) : base(notificator)
        {
            _heroRepository = heroRepository;
        }

        public async Task<bool> Add(Hero hero)
        {
            if (!Validate(new HeroValidation(), hero)) return false;

            if (_heroRepository.Search(f => f.IdMarvel == hero.IdMarvel).Result.Any())
            {
                Notificate(Models.Enums.NotificationType.Warning, "There is already a hero with this IdMarvel.");
                return false;
            }

            await _heroRepository.Add(hero);
            return true;
        }

        public async Task<bool> Update(Hero hero)
        {
            var blankHero = await _heroRepository.GetById(hero.Guid);
            hero.Id = blankHero.Id;

            if (!Validate(new HeroValidation(), hero)) return false;

            if (_heroRepository.Search(f => f.Id == hero.Id && f.IdMarvel != hero.IdMarvel).Result.Any())
            {
                Notificate(Models.Enums.NotificationType.Warning, "It's not possible to update a IdMarvel.");
                return false;
            }

            await _heroRepository.Update(hero);
            return true;
        }

        public void Dispose()
        {
            _heroRepository?.Dispose();
        }
    }
}
