using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Model
{
    public class MasterFundDTO
    {
        public string policy_seq { get; set; }
        public string policy_id { get; set; }
        public string policy_type_id { get; set; }
        public string policy_title_th { get; set; }
        public string policy_title_en { get; set; }
        public string policy_detail_th { get; set; }
        public string policy_detail_en { get; set; }
        public string show_with { get; set; }
        public string policy_pdf_name_th { get; set; }
        public string policy_pdf_url_th { get; set; }
        public string policy_pdf_original_name_th { get; set; }
        public string policy_pdf_name_en { get; set; }
        public string policy_pdf_url_en { get; set; }
        public string policy_pdf_original_name_en { get; set; }
        public string policy_goto_url_th { get; set; }
        public string policy_goto_url_en { get; set; }
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string modify_by { get; set; }
        public string modify_date { get; set; }
        public string order_by { get; set; }
        public string active { get; set; }
    }
    public class MasterFundListDTO
    {
        public string policy_seq { get; set; }
        public string policy_id { get; set; }
        public string policy_type_id { get; set; }
        public string policy_title_th { get; set; }
        public string policy_title_en { get; set; }
        public string policy_detail_th { get; set; }
        public string policy_detail_en { get; set; }
        public string policy_type_name_th { get; set; }
        public string investment_type_name { get; set; }
    }

    public class MasterFundSearchListDTO
    {
        public string InvestorPage { get; set; }
    }

}
