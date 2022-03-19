using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MarvelHeroes.Api.Controllers;
using MarvelHeroes.Api.ViewModels;
using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models;
using MarvelHeroes.Business.Models.Enums;
using MarvelHeroes.Integração.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarvelHeroes.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/hero")]
    public class HeroController : MainController
    {
        private readonly IHeroService _heroService;
        private readonly IHeroRepository _heroRepository;
        private readonly IHeroIntegrationRepository _heroIntegrationRepository;
        private readonly IHqIntegrationRepository _hqIntergrationRepository;
        private readonly IMapper _mapper;

        public HeroController(IHeroService heroService,
                                      IHeroRepository heroRepository,
                                      IHeroIntegrationRepository heroIntegrationRepository,
                                      IHqIntegrationRepository hqIntegrationRepository,
                                      IMapper mapper, 
                                      INotificator notificator, 
                                      IUser user) : base(notificator, user, mapper)
        {
            _heroRepository = heroRepository;
            _heroIntegrationRepository = heroIntegrationRepository;
            _hqIntergrationRepository = hqIntegrationRepository;
            _heroService = heroService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todos os personagens cadastrados no banco de dados da aplicação
        /// </summary>
        /// <returns>Lista de personagens cadastrados</returns>
        [HttpGet]
        public async Task<IEnumerable<HeroViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<HeroViewModel>>(await _heroRepository.GetAll());
        }

        /// <summary>
        /// Lista os personagens disponiveis na API pública da Marvel utilizando parametros de paginação
        /// </summary>
        /// <param name="limit">Quantidade de personagens a serem recuperados</param>
        /// <param name="offset">Quantidade de personagens a serem pulados</param>
        /// <returns>Lista de personagens</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("integration/&limit={limit:int}&offset={offset:int}")]
        public dynamic ListHerosOnMarvelIntegration(int limit, int offset)
        {
            var heros =  _heroIntegrationRepository.ListHeros(limit, offset);
            heros.list = _mapper.Map<List<HeroIntegrationViewModel>>(heros.list);
            return CustomResponse(heros);
        }

        /// <summary>
        /// Obtém um único personagem na API pública da Marvel
        /// </summary>
        /// <param name="idMarvel">Id Marvel do personagem</param>
        /// <returns>Um único personagem Marvel</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("integration/{idMarvel:int}")]
        public dynamic GetHeroOnMarvelIntegration(int idMarvel)
        {
            return CustomResponse(_mapper.Map<HeroIntegrationViewModel>(_heroIntegrationRepository.GetHero(idMarvel)));
        }

        /// <summary>
        /// Lista os quadrinhos que envolvem o personagem da Marvel informado
        /// </summary>
        /// <param name="idMarvel">Id Marvel do personagem</param>
        /// <param name="limit">Quantidade de quadrinhos a serem listados</param>
        /// <param name="offset">Quantidade de quadrinhos a serem pulados</param>
        /// <returns>Lista de quadrinhos</returns>limit
        [HttpGet]
        [AllowAnonymous]
        [Route("integration/{idMarvel:int}/hq/&limit={limit:int}&offset={offset:int}")]
        public dynamic GetHeroHqOnMarvelIntegration(int idMarvel, int limit, int offset)
        {
            var hqs = _hqIntergrationRepository.GetHeroHq(idMarvel, limit, offset);
            hqs.list = _mapper.Map<List<HqIntegrationViewModel>>(hqs.list);
            return CustomResponse(hqs);
        }

        /// <summary>
        /// Obtém um personagem cadastrado no banco de dados da aplicação com base no Id informado
        /// </summary>
        /// <param name="guid">Id do personagem</param>
        /// <returns>Personagem único cadastrado</returns>
        [HttpGet("{guid:guid}")]
        public async Task<ActionResult<HeroViewModel>> GetById(Guid guid)
        {
            var hero = await GetHeroById(guid);

            if (hero == null) {
                Notificate(NotificationType.Warning, "It was not possible the get the desired hero");
                return CustomResponse(null, 404);
            } 

            return hero;
        }

        /// <summary>
        /// Salva um personagem no banco de dados da aplicação
        /// </summary>
        /// <param name="heroViewModel">Personagem a ser salvo</param>
        /// <returns>Personagem salvo</returns>
        [HttpPost]
        public async Task<ActionResult<HeroViewModel>> Add(HeroIntegrationViewModel heroViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _heroService.Add(_mapper.Map<Hero>(heroViewModel));

            return CustomResponse(heroViewModel);
        }

        /// <summary>
        /// Atualiza um personagem no banco de dados da aplicação
        /// </summary>
        /// <param name="guid">Id do personagem a ser atualizado</param>
        /// <param name="heroViewModel">Personagem a ser atualizado</param>
        /// <returns>Personagem atualizado</returns>
        [HttpPut("{guid:guid}")]
        public async Task<ActionResult<HeroViewModel>> Update(Guid guid, HeroViewModel heroViewModel)
        {
            if (guid != heroViewModel.Guid)
            {
                Notificate(NotificationType.Warning, "The informed Id is not the same informaded in the URL");
                return CustomResponse(heroViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var hero = _mapper.Map<Hero>(heroViewModel);

            await _heroService.Update(hero);

            return CustomResponse(heroViewModel);
        }

        /// <summary>
        /// Excluí um personagem do banco de dados da aplicação
        /// </summary>
        /// <param name="guid">Id do personagem a ser excluído</param>
        /// <returns>Personagem excluído</returns>
        [HttpDelete("{guid:guid}")]
        public async Task<ActionResult<HeroViewModel>> Delete(Guid guid)
        {
            var hero = await GetHeroById(guid);

            if (hero == null)
            {
                Notificate(NotificationType.Warning, "It was not possible the get the desired hero");
                return CustomResponse(null, 404);
            }

            await _heroRepository.RemoveById(hero.Guid);

            return CustomResponse(hero);
        }

        private async Task<HeroViewModel> GetHeroById(Guid guid)
        {
            return _mapper.Map<HeroViewModel>(await _heroRepository.GetById(guid));
        }
    }
}