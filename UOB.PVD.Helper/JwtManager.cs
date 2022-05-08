using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Helper
{
    public class JwtManager
    {
        public static bool IsEmptyOrInvalid(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return true;
                }
 
                var jwtToken = new JwtSecurityToken(token);
                return (jwtToken == null) || (jwtToken.ValidFrom > DateTime.UtcNow) || (jwtToken.ValidTo < DateTime.UtcNow);
            }
            catch
            {
                return true;
            }
        }
    }
}
