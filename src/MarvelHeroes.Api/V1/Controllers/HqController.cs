using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MarvelHeroes.Api.Controllers;
using MarvelHeroes.Api.ViewModels;
using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models;
using MarvelHeroes.Business.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarvelHeroes.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/hq")]
    public class HqController : MainController
    {
        private readonly IHqRepository _hqRepository;
        private readonly IMapper _mapper;

        public HqController(IHqRepository hqRepository, 
                                      IMapper mapper, 
                                      INotificator notificator, 
                                      IUser user) : base(notificator, user, mapper)
        {
            _hqRepository = hqRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os quadrinhos cadastrados no banco de dados da aplicação
        /// </summary>
        /// <returns>Lista de quadrinhos cadastrados</returns>
        [HttpGet]
        public async Task<IEnumerable<HqViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<HqViewModel>>(await _hqRepository.GetAll());
        }

        /// <summary>
        /// Obtém um quadrinho cadastrado no banco de dados da aplicação por Id
        /// </summary>
        /// <param name="guid">Id do quadrinho cadastrado</param>
        /// <returns>Um único quadrinho Marvel</returns>
        [HttpGet("{guid:guid}")]
        public async Task<ActionResult<HqViewModel>> GetById(Guid guid)
        {
            var quadrinhoViewModel = await GetHqById(guid);

            if (quadrinhoViewModel == null) {
                Notificate(NotificationType.Warning, "It was not possible the get the desired hq");
                return CustomResponse(null, 404);
            } 

            return quadrinhoViewModel;
        }

        /// <summary>
        /// Salva um quadrinho no banco de dados da aplicação 
        /// </summary>
        /// <param name="hqViewModel">Quadrinho a ser salvo</param>
        /// <returns>Quadrinho salvo</returns>
        [HttpPost]
        public async Task<ActionResult<HqViewModel>> Add(HqViewModel hqViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _hqRepository.Add(_mapper.Map<Hq>(hqViewModel));

            return CustomResponse(hqViewModel);
        }

        /// <summary>
        /// Atualiza um quadrinho salvo no banco de dados
        /// </summary>
        /// <param name="guid">Id do quadrinho a ser atualizado</param>
        /// <param name="hqViewModel">Quadrinho a ser atualizado</param>
        /// <returns>Quadrinho atualizado</returns>
        [HttpPut("{guid:guid}")]
        public async Task<ActionResult<HqViewModel>> Update(Guid guid, HqViewModel hqViewModel)
        {
            if (guid != hqViewModel.Guid)
            {
                Notificate(NotificationType.Warning, "The informed Id is not the same informaded in the URL");
                return CustomResponse(hqViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _hqRepository.Update(_mapper.Map<Hq>(hqViewModel));

            return CustomResponse(hqViewModel);
        }

        /// <summary>
        /// Excluí um quadrinho do banco de dados da aplicação
        /// </summary>
        /// <param name="guid">Id do quadrinho a ser excluído</param>
        /// <returns>Quadrinho excluído</returns>
        [HttpDelete("{guid:guid}")]
        public async Task<ActionResult<HqViewModel>> Delete(Guid guid)
        {
            var hq = await GetHqById(guid);

            if (hq == null) return NotFound();

            await _hqRepository.RemoveById(hq.Guid);

            return CustomResponse(hq);
        }

        private async Task<HqViewModel> GetHqById(Guid guid)
        {
            return _mapper.Map<HqViewModel>(await _hqRepository.GetById(guid));
        }
    }
}