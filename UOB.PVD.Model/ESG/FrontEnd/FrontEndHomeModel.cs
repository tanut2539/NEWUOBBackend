using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Model.ESG.FrontEnd
{
    public class FrontEndHomeResponseModel : BaseResponse
    {
        public string current_article_size { get; set; }
        public List<FrontEndBannerModel> Banner { get; set; }
        public List<FrontEndArticleModel> Article { get; set; }
        public List<FrontEndFundModel> Fund { get; set; }
    }
    public class FrontEndHomeModel
    {
        public string current_article_size { get; set; }
        public List<FrontEndBannerModel> Banner { get; set; }
        public List<FrontEndArticleModel> Article { get; set; }
        public List<FrontEndFundModel> Fund { get; set; }
    }
    public class FrontEndArticleDetailModel
    {
        public string thisURL { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public DateTime publish_date { get; set; }
        public string str_publish_date { get; set; }
        public string create_by_name { get; set; }
    }
    

}
