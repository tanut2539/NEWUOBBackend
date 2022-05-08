using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Model.ESG
{
    public class FundModel
    {
        public string fund_seq { get; set; }
        public string title_th { get; set; }
        public string title_en { get; set; }
        public string text_en { get; set; }
        public string text_th { get; set; }
        public string image_th { get; set; }
        public string url_image_th { get; set; }
        public string image_en { get; set; }
        public string url_image_en { get; set; }
        public string link { get; set; }
        public string status { get; set; }
        public string status_name { get; set; }
        public string order_by { get; set; }

    }
    public class DatataFundResponseModel : BaseResponse
    {
        public int recordsFiltered { get; set; }
        public List<DatataFundResultModel> data { get; set; }
    }

    public class DatataFundResultModel
    {
        public string fund_seq { get; set; }
        public string rowindex { get; set; }
        public string maxrow { get; set; }
        public string image_th { get; set; }
        public string image_en { get; set; }
        public string link { get; set; }
        public string status { get; set; }
        public string status_name { get; set; }
    }

    public class DatatableSearchFund : BaseDatatableSearch
    {

    }

    public class FrontEndFundModel
    {
        public string fund_seq { get; set; }
        public string rowindex { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string image { get; set; }
        public string url_image { get; set; }
        public string link { get; set; }
        public string order_by { get; set; }

    }
    public class FundEditOrderModel
    {
        public List<string> fund_seq { get; set; }
    }
}
