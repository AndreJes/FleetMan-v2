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
        public async Task<UserInfo> ValidateLogin(string email, string password)
        {
            try
            {
                FleetManHash fleetManHash = new FleetManHash();

                using (FleetManContext context = new FleetManContext())
                {
                    //Recover Salt string from Database
                    LoginModel loginData = await context.LoginDatas.Where(ld => ld.Email == email).FirstOrDefaultAsync();

                    if(loginData == null)
                    {
                        return new UserInfo();
                    }

                    string validation_hash = fleetManHash.GetHashedPassword(password, loginData.Salt);

                    if(validation_hash.Equals(loginData.Password))
                    {
                        return new UserInfo(is_valid:true, role:loginData.Role, cnpj:loginData.Login);
                    }

                    return new UserInfo();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class UserInfo
    {
        public bool IsValid { get; set; }
        public string Role { get; set; }
        public string Cnpj { get; set; }

        public UserInfo()
        {
            this.IsValid = false;
            this.Role = "";
            this.Cnpj = "";
        }

        public UserInfo(bool is_valid, string role, string cnpj)
        {
            this.IsValid = is_valid;
            this.Role = role;
            this.Cnpj = cnpj;
        }
    }
}
