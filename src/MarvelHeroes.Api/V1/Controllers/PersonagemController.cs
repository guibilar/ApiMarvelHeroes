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
    [Route("api/v{version:apiVersion}/personagem")]
    public class PersonagemController : MainController
    {
        private readonly IPersonagemService _personagemService;
        private readonly IPersonagemRepository _personagemRepository;
        private readonly IPersonagemIntegracaoRepository _integracaoPersonagemMarvel;
        private readonly IQuadrinhoIntegracaoRepository _integracaoQuadrinhoMarvel;
        private readonly IMapper _mapper;

        public PersonagemController(IPersonagemService personagemService,
                                      IPersonagemRepository personagemRepository,
                                      IPersonagemIntegracaoRepository integracaoPersonagemMarvel,
                                      IQuadrinhoIntegracaoRepository integracaoQuadrinhoMarvel,
                                      IMapper mapper, 
                                      INotificador notificador, 
                                      IUser user) : base(notificador, user, mapper)
        {
            _personagemRepository = personagemRepository;
            _integracaoPersonagemMarvel = integracaoPersonagemMarvel;
            _integracaoQuadrinhoMarvel = integracaoQuadrinhoMarvel;
            _personagemService = personagemService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todos os personagens cadastrados no banco de dados da aplicação
        /// </summary>
        /// <returns>Lista de personagens cadastrados</returns>
        [HttpGet]
        public async Task<IEnumerable<PersonagemViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<PersonagemViewModel>>(await _personagemRepository.ObterTodos());
        }

        /// <summary>
        /// Lista os personagens disponiveis na API pública da Marvel utilizando parametros de paginação
        /// </summary>
        /// <param name="limite">Quantidade de personagens a serem recuperados</param>
        /// <param name="offset">Quantidade de personagens a serem pulados</param>
        /// <returns>Lista de personagens</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("integracao/&limite={limite:int}&offset={offset:int}")]
        public dynamic ListarViaIntegração(int limite, int offset)
        {
            var resultado =  _integracaoPersonagemMarvel.ListaPersonagens(limite, offset);
            resultado.lista = _mapper.Map<List<PersonagemIntegracaoViewModel>>(resultado.lista);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Obtém um único personagem na API pública da Marvel
        /// </summary>
        /// <param name="idMarvel">Id Marvel do personagem</param>
        /// <returns>Um único personagem Marvel</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("integracao/{idMarvel:int}")]
        public dynamic ObterPersonagemViaIntegração(int idMarvel)
        {
            return CustomResponse(_mapper.Map<PersonagemIntegracaoViewModel>(_integracaoPersonagemMarvel.ObterPersonagem(idMarvel)));
        }

        /// <summary>
        /// Lista os quadrinhos que envolvem o personagem da Marvel informado
        /// </summary>
        /// <param name="idMarvel">Id Marvel do personagem</param>
        /// <param name="limite">Quantidade de quadrinhos a serem listados</param>
        /// <param name="offset">Quantidade de quadrinhos a serem pulados</param>
        /// <returns>Lista de quadrinhos</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("integracao/{idMarvel:int}/quadrinhos/&limite={limite:int}&offset={offset:int}")]
        public dynamic ObterQuadrinhosDoPersonagemViaIntegração(int idMarvel, int limite, int offset)
        {
            var resultado = _integracaoQuadrinhoMarvel.ListaQuadrinhosDePersonagem(idMarvel, limite, offset);
            resultado.lista = _mapper.Map<List<QuadrinhontegracaoViewModel>>(resultado.lista);
            return CustomResponse(resultado);
        }

        /// <summary>
        /// Obtém um personagem cadastrado no banco de dados da aplicação com base no Id informado
        /// </summary>
        /// <param name="guid">Id do personagem</param>
        /// <returns>Personagem único cadastrado</returns>
        [HttpGet("{guid:guid}")]
        public async Task<ActionResult<PersonagemViewModel>> ObterPorId(Guid guid)
        {
            var fornecedor = await ObterPersonagemPorGuid(guid);

            if (fornecedor == null) {
                Notificar(TipoNotificacao.Aviso, "Não foi possivel localizar o personagem informado");
                return CustomResponse(null, 404);
            } 

            return fornecedor;
        }

        /// <summary>
        /// Salva um personagem no banco de dados da aplicação
        /// </summary>
        /// <param name="personagemViewModel">Personagem a ser salvo</param>
        /// <returns>Personagem salvo</returns>
        [HttpPost]
        public async Task<ActionResult<PersonagemViewModel>> Adicionar(PersonagemIntegracaoViewModel personagemViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _personagemService.Adicionar(_mapper.Map<Personagem>(personagemViewModel));

            return CustomResponse(personagemViewModel);
        }

        /// <summary>
        /// Atualiza um personagem no banco de dados da aplicação
        /// </summary>
        /// <param name="guid">Id do personagem a ser atualizado</param>
        /// <param name="personagemViewModel">Personagem a ser atualizado</param>
        /// <returns>Personagem atualizado</returns>
        [HttpPut("{guid:guid}")]
        public async Task<ActionResult<PersonagemViewModel>> Atualizar(Guid guid, PersonagemViewModel personagemViewModel)
        {
            if (guid != personagemViewModel.Guid)
            {
                Notificar(TipoNotificacao.Aviso, "O id informado não é o mesmo que foi passado como parâmetro");
                return CustomResponse(personagemViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var personagemMapeado = _mapper.Map<Personagem>(personagemViewModel);

            await _personagemService.Atualizar(personagemMapeado);

            return CustomResponse(personagemViewModel);
        }

        /// <summary>
        /// Excluí um personagem do banco de dados da aplicação
        /// </summary>
        /// <param name="guid">Id do personagem a ser excluído</param>
        /// <returns>Personagem excluído</returns>
        [HttpDelete("{guid:guid}")]
        public async Task<ActionResult<PersonagemViewModel>> Excluir(Guid guid)
        {
            var personagemViewModel = await ObterPersonagemPorGuid(guid);

            if (personagemViewModel == null)
            {
                Notificar(TipoNotificacao.Aviso, "Não foi possivel localizar o personagem informado");
                return CustomResponse(null, 404);
            }

            await _personagemRepository.RemoverPorGuid(personagemViewModel.Guid);

            return CustomResponse(personagemViewModel);
        }

        private async Task<PersonagemViewModel> ObterPersonagemPorGuid(Guid guid)
        {
            return _mapper.Map<PersonagemViewModel>(await _personagemRepository.ObterPorGuid(guid));
        }

        private async Task<int> ObterIdPersonagem(Guid guid)
        {
            return (await _personagemRepository.ObterPorGuid(guid)).Id;
        }
    }
}