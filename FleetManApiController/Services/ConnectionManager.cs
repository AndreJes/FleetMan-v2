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

                context.Database.Connection.Open();
                context.Database.Connection.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
