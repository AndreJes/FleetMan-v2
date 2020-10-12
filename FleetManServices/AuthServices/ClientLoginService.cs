using FleetManDAL.Contexts;
using FleetManDAL.DAOs;
using FleetManModel.Classes;
using FleetManModel.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManServices.AuthServices
{
    public class ClientLoginService
    {
        public async Task<bool> ValidateLogin(string email, string password)
        {
            try
            {
                FleetManHash fleetManHash = new FleetManHash();

                using (FleetManContext context = new FleetManContext())
                {
                    //Recover Salt string from Database
                    LoginData loginData = await context.LoginDatas.Where(ld => ld.Email == email).FirstOrDefaultAsync();

                    if(loginData == null)
                    {
                        return false;
                    }

                    string validation_hash = fleetManHash.GetHashedPassword(password, loginData.Salt);

                    if(validation_hash.Equals(loginData.Password))
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
