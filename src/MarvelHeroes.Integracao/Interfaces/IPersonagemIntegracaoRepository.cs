using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes.Integração.Interfaces
{
    public interface IPersonagemIntegracaoRepository
    {
        public dynamic ListaPersonagens(int limite, int offset);

        public dynamic ObterPersonagem(int idMarvel);
    }
}
