using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wipro.ConsoleApp.Model
{
    public class MoedaResultErro
    {
        public bool Sucesso { get; set; }
        public string Erro { get; set; }
    }

    public class MoedaResultOk
    {
        public bool Sucesso { get; set; }
        public object Data { get; set; }
    }
}
