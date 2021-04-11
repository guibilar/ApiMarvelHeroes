using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes.Integração.Helpers
{
    public class ResultadoPaginado
    {
        public List<object> Lista { get; set; }
        public int Total { get; set; }
        public int Tamanho { get; set; }
        public int OffSet { get; set; }
    }
}
