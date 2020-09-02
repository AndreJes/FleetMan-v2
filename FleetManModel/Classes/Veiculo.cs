using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManModel.Classes
{
    public class Veiculo
    {
        [Key]
        public string Placa { get; set; }
        [Index(IsUnique = true)]
        public string Renavam { get; set; }
        [Index(IsUnique = true)]
        public string Chassi { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Marca { get; set; }

        public bool Ativo { get; set; }

        public virtual Cliente Cliente { get; set; }

    }
}
