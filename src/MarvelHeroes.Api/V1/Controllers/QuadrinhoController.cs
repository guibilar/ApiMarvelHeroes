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
    [Route("api/v{version:apiVersion}/quadrinho")]
    public class QuadrinhoController : MainController
    {
        private readonly IQuadrinhoRepository _quadrinhoRepository;
        private readonly IMapper _mapper;

        public QuadrinhoController(IQuadrinhoRepository quadrinhoRepository, 
                                      IMapper mapper, 
                                      INotificador notificador, 
                                      IUser user) : base(notificador, user, mapper)
        {
            _quadrinhoRepository = quadrinhoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os quadrinhos cadastrados no banco de dados da aplicação
        /// </summary>
        /// <returns>Lista de quadrinhos cadastrados</returns>
        [HttpGet]
        public async Task<IEnumerable<QuadrinhoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<QuadrinhoViewModel>>(await _quadrinhoRepository.ObterTodos());
        }

        /// <summary>
        /// Obtém um quadrinho cadastrado no banco de dados da aplicação por Id
        /// </summary>
        /// <param name="guid">Id do quadrinho cadastrado</param>
        /// <returns>Um único quadrinho Marvel</returns>
        [HttpGet("{guid:guid}")]
        public async Task<ActionResult<QuadrinhoViewModel>> ObterPorId(Guid guid)
        {
            var quadrinhoViewModel = await ObterQuadrinhoPorGuid(guid);

            if (quadrinhoViewModel == null) {
                Notificar(TipoNotificacao.Aviso, "Não foi possivel localizar o quadrinho informado");
                return CustomResponse(null, 404);
            } 

            return quadrinhoViewModel;
        }

        /// <summary>
        /// Salva um quadrinho no banco de dados da aplicação 
        /// </summary>
        /// <param name="quadrinhoViewModel">Quadrinho a ser salvo</param>
        /// <returns>Quadrinho salvo</returns>
        [HttpPost]
        public async Task<ActionResult<QuadrinhoViewModel>> Adicionar(QuadrinhoViewModel quadrinhoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _quadrinhoRepository.Adicionar(_mapper.Map<Quadrinho>(quadrinhoViewModel));

            return CustomResponse(quadrinhoViewModel);
        }

        /// <summary>
        /// Atualiza um quadrinho salvo no banco de dados
        /// </summary>
        /// <param name="guid">Id do quadrinho a ser atualizado</param>
        /// <param name="quadrinhoViewModel">Quadrinho a ser atualizado</param>
        /// <returns>Quadrinho atualizado</returns>
        [HttpPut("{guid:guid}")]
        public async Task<ActionResult<QuadrinhoViewModel>> Atualizar(Guid guid, QuadrinhoViewModel quadrinhoViewModel)
        {
            if (guid != quadrinhoViewModel.Guid)
            {
                Notificar(TipoNotificacao.Aviso, "O id informado não é o mesmo que foi passado como parâmetro");
                return CustomResponse(quadrinhoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _quadrinhoRepository.Atualizar(_mapper.Map<Quadrinho>(quadrinhoViewModel));

            return CustomResponse(quadrinhoViewModel);
        }

        /// <summary>
        /// Excluí um quadrinho do banco de dados da aplicação
        /// </summary>
        /// <param name="guid">Id do quadrinho a ser excluído</param>
        /// <returns>Quadrinho excluído</returns>
        [HttpDelete("{guid:guid}")]
        public async Task<ActionResult<QuadrinhoViewModel>> Excluir(Guid guid)
        {
            var quadrinhoViewModel = await ObterQuadrinhoPorGuid(guid);

            if (quadrinhoViewModel == null) return NotFound();

            await _quadrinhoRepository.RemoverPorGuid(quadrinhoViewModel.Guid);

            return CustomResponse(quadrinhoViewModel);
        }

        private async Task<QuadrinhoViewModel> ObterQuadrinhoPorGuid(Guid guid)
        {
            return _mapper.Map<QuadrinhoViewModel>(await _quadrinhoRepository.ObterPorGuid(guid));
        }
    }
}