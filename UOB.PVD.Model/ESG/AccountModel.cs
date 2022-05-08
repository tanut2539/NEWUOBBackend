using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Model.ESG
{
    public class AuthenticationModel
    {
        public string username { get; set; }
        public DateTime ExpiresTime { get; set; }
    }
    public class LoginDTO
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class AccountModel
    {
 
        public string Token { get; set; }
        public DateTime ExpiresTime { get; set; }
    }
}
