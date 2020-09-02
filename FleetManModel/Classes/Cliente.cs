using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace FleetManModel.Classes
{
    public class Cliente
    {
        [Key]
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }

        public bool Ativo { get; set; }

        public virtual List<Veiculo> Veiculos { get; set; }
        public virtual List<Motorista> Motoristas { get; set; }
    }
}
