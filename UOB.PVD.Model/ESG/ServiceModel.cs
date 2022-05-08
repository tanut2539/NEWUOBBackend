using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Model.ESG
{
    public class ServiceModel
    {
    }
    public class ServiceHeaderModel
    {
        public ServiceHeaderModel()
        {
            Header = new List<ServiceHeaderData>();
        }
        public List<ServiceHeaderData> Header { get; set; }
    }
    public class ServiceHeaderData
    {
        public string HeaderName { get; set; }
        public string HeaderValue { get; set; }
    }
}
