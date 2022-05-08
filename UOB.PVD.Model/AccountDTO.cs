using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Model
{
    public class AccountDTO
    {

    }
    public class LoginDTO
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class LoginResponseDTO
    {
        public string name { get; set; }
        public string token { get; set; }
        public DateTime expire_time { get; set; }
    }
    public class AccountModel
    {
        public string user_seq { get; set; }
        public string username { get; set; }
        public string user_fname { get; set; }
        public string user_lname { get; set; }
    }
}
