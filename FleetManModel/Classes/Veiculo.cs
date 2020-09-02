﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManModel.Classes
{
    public class Veiculo
    {
        [Key]
        public string Placa { get; set; }
        public string Renavam { get; set; }
        public string Chassi { get; set; }
        public string Modelo { get; set; }
        public string Ano { get; set; }
        public string Marca { get; set; }

        public bool Ativo { get; set; }

        public virtual Cliente Cliente { get; set; }

    }
}
