using FleetManDAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManApiController.Services
{
    public class ConnectionManager
    {
        public static bool CheckDbConnection()
        {
            try
            {
                using FleetManContext context = new FleetManContext();

                return context.Database.Exists();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
