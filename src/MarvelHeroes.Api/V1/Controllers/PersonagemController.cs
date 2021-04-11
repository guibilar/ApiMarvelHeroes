using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MarvelHeroes.Api.Controllers;
using MarvelHeroes.Api.ViewModels;
using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models;
using MarvelHeroes.Integração.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarvelHeroes.Api.V1.Controllers
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/personagem")]
    public class PersonagemController : MainController
    {
        private readonly IPersonagemRepository _personagemRepository;
        private readonly IPersonagemIntegracaoRepository _integracaoPersonagemMarvel;
        private readonly IQuadrinhoIntegracaoRepository _integracaoQuadrinhoMarvel;
        private readonly IMapper _mapper;

        public PersonagemController(IPersonagemRepository personagemRepository,
                                      IPersonagemIntegracaoRepository integracaoPersonagemMarvel,
                                      IQuadrinhoIntegracaoRepository integracaoQuadrinhoMarvel,
                                      IMapper mapper, 
                                      INotificador notificador, 
                                      IUser user) : base(notificador, user)
        {
            _personagemRepository = personagemRepository;
            _integracaoPersonagemMarvel = integracaoPersonagemMarvel;
            _integracaoQuadrinhoMarvel = integracaoQuadrinhoMarvel;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PersonagemViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<PersonagemViewModel>>(await _personagemRepository.ObterTodos());
        }

        [HttpGet]
        [Route("integracao/&limite={limite:int}&offset={offset:int}")]
        public dynamic ListarViaIntegração(int limite, int offset)
        {
            var resultado =  _integracaoPersonagemMarvel.ListaPersonagens(limite, offset);
            resultado.lista = _mapper.Map<List<PersonagemIntegracaoViewModel>>(resultado.lista);
            return CustomResponse(resultado);
        }

        [HttpGet]
        [Route("integracao/{idMarvel:int}")]
        public dynamic ObterPersonagemViaIntegração(int idMarvel)
        {
            return CustomResponse(_mapper.Map<PersonagemIntegracaoViewModel>(_integracaoPersonagemMarvel.ObterPersonagem(idMarvel)));
        }

        [HttpGet]
        [Route("integracao/{idMarvel:int}/comics/&limite={limite:int}&offset={offset:int}")]
        public dynamic ObterComicsDoPersonagemViaIntegração(int idMarvel, int limite, int offset)
        {
            var resultado = _integracaoQuadrinhoMarvel.ListaQuadrinhosDePersonagem(idMarvel, limite, offset);
            resultado.lista = _mapper.Map<List<QuadrinhontegracaoViewModel>>(resultado.lista);
            return CustomResponse(resultado);
        }

        [HttpGet("{guid:guid}")]
        public async Task<ActionResult<PersonagemViewModel>> ObterPorId(Guid guid)
        {
            var fornecedor = await ObterPersonagemPorGuid(guid);

            if (fornecedor == null) {
                NotificarAviso("Não foi possivel localizar o personagem informado");
                return CustomResponse(null, 404);
            } 

            return fornecedor;
        }

        [HttpPost]
        public async Task<ActionResult<PersonagemViewModel>> Adicionar(PersonagemIntegracaoViewModel personagemViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _personagemRepository.Adicionar(_mapper.Map<Personagem>(personagemViewModel));

            return CustomResponse(personagemViewModel);
        }

        [HttpPut("{guid:guid}")]
        public async Task<ActionResult<PersonagemViewModel>> Atualizar(Guid guid, PersonagemViewModel personagemViewModel)
        {
            if (guid != personagemViewModel.Guid)
            {
                NotificarAviso("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(personagemViewModel);
            }

            var id = await ObterIdPersonagem(guid);

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var personagemMapeado = _mapper.Map<Personagem>(personagemViewModel);
            personagemMapeado.Id = id;

            await _personagemRepository.Atualizar(personagemMapeado);

            return CustomResponse(personagemViewModel);
        }

        [HttpDelete("{guid:guid}")]
        public async Task<ActionResult<PersonagemViewModel>> Excluir(Guid guid)
        {
            var personagemViewModel = await ObterPersonagemPorGuid(guid);

            if (personagemViewModel == null)
            {
                NotificarAviso("Não foi possivel localizar o personagem informado");
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