using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManModel.Classes
{
    public class LoginData
    {
        [Key]
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
