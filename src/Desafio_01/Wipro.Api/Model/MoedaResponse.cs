using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wipro.Api.Model
{
    public class MoedaResponse
    {
        public MoedaResponse()
        {
            ListaIncluidoComSucesso = new List<MoedaModel>();
        }

        public List<MoedaModel> ListaIncluidoComSucesso { get; set; }
    }
}
