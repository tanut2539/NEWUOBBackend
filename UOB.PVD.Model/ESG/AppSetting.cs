using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Model.ESG
{
    public class AppSetting
    {
        public string Secret { get; set; }
        public int ExpiresTime { get; set; }
        public string CMS_URL { get; set; }
        public string FRONT_URL { get; set; }
        public string API_URL { get; set; }

    }
}
