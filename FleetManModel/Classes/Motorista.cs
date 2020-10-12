using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManModel.Classes
{
    public class Motorista
    {
        [Key]
        public string CPF { get; set; }
        public string CNH { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public Endereco Endereco { get; set; }
        public bool Ativo { get; set; }

        [ForeignKey("Cliente")]
        public virtual string Cliente_CNPJ { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
