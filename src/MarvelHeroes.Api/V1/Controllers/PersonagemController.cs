using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MarvelHeroes.Api.Controllers;
using MarvelHeroes.Api.ViewModels;
using MarvelHeroes.Business.Intefaces;
using MarvelHeroes.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.V1.Controllers
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/personagens")]
    public class PersonagemController : MainController
    {
        private readonly IPersonagemRepository _personagemRepository;
        private readonly IMapper _mapper;

        public PersonagemController(IPersonagemRepository personagemRepository, 
                                      IMapper mapper, 
                                      INotificador notificador, 
                                      IUser user) : base(notificador, user)
        {
            _personagemRepository = personagemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PersonagemViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<PersonagemViewModel>>(await _personagemRepository.ObterTodos());
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
        public async Task<ActionResult<PersonagemViewModel>> Adicionar(PersonagemViewModel personagemViewModel)
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

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _personagemRepository.Atualizar(_mapper.Map<Personagem>(personagemViewModel));

            return CustomResponse(personagemViewModel);
        }

        [HttpDelete("{guid:guid}")]
        public async Task<ActionResult<PersonagemViewModel>> Excluir(Guid guid)
        {
            var personagemViewModel = await ObterPersonagemPorGuid(guid);

            if (personagemViewModel == null) return NotFound();

            await _personagemRepository.RemoverPorGuid(personagemViewModel.Guid);

            return CustomResponse(personagemViewModel);
        }

        private async Task<PersonagemViewModel> ObterPersonagemPorGuid(Guid guid)
        {
            return _mapper.Map<PersonagemViewModel>(await _personagemRepository.ObterPorGuid(guid));
        }
    }
}