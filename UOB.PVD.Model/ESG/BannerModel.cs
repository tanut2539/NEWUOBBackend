using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Model.ESG
{
    public class BannerModel
    {
        public string banner_seq { get; set; }
        public string title_th { get; set; }
        public string title_en { get; set; }
        public string text_th { get; set; }
        public string text_en { get; set; }
        public string link { get; set; }
        public string image_th { get; set; }
        public string url_image_th { get; set; }
        public string image_en { get; set; }
        public string url_image_en { get; set; }
        public string image_mobile_th { get; set; }
        public string url_image_mobile_th { get; set; }
        public string image_mobile_en { get; set; }
        public string url_image_mobile_en { get; set; }

        public string vdo { get; set; }
        public string url_vdo { get; set; }
        public string status { get; set; }
        public string status_name { get; set; }

    }
    public class DatataBannerResponseModel
    {
 
        public int recordsFiltered { get; set; }
        public List<DatataBannerResultModel> data { get; set; }
    }

    public class DatataBannerResultModel
    {
        public string rowindex { get; set; }
        public string maxrow { get; set; }
        public string title_th { get; set; }
        public string banner_seq { get; set; }
        public string title_en { get; set; }
        public string image_th { get; set; }
        public string status { get; set; }
        public string status_name { get; set; }
        public string vdo { get; set; }
    }

    public class DatatableSearchBanner: BaseDatatableSearch
    {

    }

    public class FrontEndBannerModel
    {
        public string banner_seq { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string link { get; set; }
        public string image { get; set; }
        public string url_image { get; set; }
        public string image_mobile { get; set; }
        public string url_image_mobile { get; set; }
        public string url_vdo { get; set; }
    }


}
