using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManModel.Classes
{
    [ComplexType]
    public class Endereco
    {
        public string Logradouro { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
    }
}
