using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Model.FrontEnd
{
    public class HomeDTO
    {
        public FrontEndFundDTO UOBInvestorChoice { get; set; } //Tab1
        public FrontEndFundDTO UOBMasterFund { get; set; } //Tab2
    }

    public class FrontEndFundDTO
    {
        public FrontEndFundDTO()
        {
            InvestmentType = new List<FrontEndInvestmentType>();
            ChartFile = new List<FrontEndChartFile>();
        }
        public string fund_seq { get; set; }
        public string fund_name { get; set; }
        public List<FrontEndInvestmentType>  InvestmentType { get; set; } // IN/OUT
        public List<FrontEndChartFile> ChartFile { get; set; }  //Graph
    }

    public class FrontEndInvestmentType
    {
        public FrontEndInvestmentType() 
        { 
            PolicyType = new List<FrontEndPolicyType>();
        }
        public string investment_type_id { get; set; }
        public string investment_type_name { get; set; }
        public List<FrontEndPolicyType> PolicyType { get; set; }
    }

    public class FrontEndPolicyType
    {
        public FrontEndPolicyType()
        {
            Policy = new List<FrontEndPolicy>();
        }
        public string policy_type_id { get; set; }
        public string policy_type_name { get; set; }
        public List<FrontEndPolicy> Policy { get; set; }
    }

    public class FrontEndPolicy
    {
        public string policy_seq { get; set; }
        public string policy_id { get; set; }
        public string policy_title { get; set; }
        public string policy_detail { get; set; }
        public string show_with { get; set; }
        public string url { get; set; }
    }

    public class FrontEndSmk
    {
        public string smk_file_seq { get; set; }
        public string smk_file_title { get; set; }
        public string smk_file_url { get; set; }
        public string smk_type { get; set; }

    }
    public class FrontEndChartFile
    {
        public string chart_file_seq { get; set; }
        public string chart_title { get; set; }
        public string chart_file_url { get; set; }

    }

    public class FrontEndSheet
    {
        public FrontEndSheet()
        {
            UOBInvestorChoice = new List<FrontEndPolicy>();
            UOBMasterFund = new List<FrontEndPolicy>();
            Smk1 = new List<FrontEndSmk>();
            Smk2 = new List<FrontEndSmk>();
            ChartFile = new List<FrontEndChartFile>();
        }


        public List<FrontEndPolicy>  UOBInvestorChoice { get; set; }
        public List<FrontEndPolicy> UOBMasterFund { get; set; }
        public List<FrontEndSmk> Smk1 { get; set; }
        public List<FrontEndSmk> Smk2 { get; set; }
        public List<FrontEndChartFile> ChartFile { get; set; }
    }
 
}
