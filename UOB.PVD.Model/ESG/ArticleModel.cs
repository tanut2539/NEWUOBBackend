using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Model.ESG
{
    public class ArticleModel
    {
        public string article_seq { get; set; }
        public string title_th { get; set; }
        public string title_en { get; set; }
        public string content_th { get; set; }
        public string content_en { get; set; }
        public string author_name { get; set; }
        public string image_th { get; set; }
        public string url_image_th { get; set; }
        public string image_en { get; set; }
        public string url_image_en { get; set; }
        public string image_thumbnail_th { get; set; }
        public string url_image_thumbnail_th { get; set; }
        public string image_thumbnail_en { get; set; }
        public string url_image_thumbnail_en { get; set; }
        public string status { get; set; }
        public string status_name { get; set; }

    }
    public class DatatableSearchArticle : BaseDatatableSearch
    {

    }

    public class DatataArticleResponseModel : BaseResponse
    {
        public int recordsFiltered { get; set; }
        public List<DatataArticleResultModel> data { get; set; }
    }

    public class DatataArticleResultModel
    {
        public string rowindex { get; set; }
        public string article_seq { get; set; }
        public string title_th { get; set; }
        public string title_en { get; set; }
        public string image_th { get; set; }
        public string status { get; set; }
        public string status_name { get; set; }
    }

    public class FrontEndArticleModel
    {

        public string article_seq { get; set; }
        public string rowindex { get; set; }
        public string maxrow { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string image { get; set; }
        public string url_image { get; set; }
        public string image_mobile { get; set; }
        public string url_image_mobile { get; set; }

    }

    public class FrontEndLoadMoreArticleModel
    {
        public string current_article_size { get; set; }
    }

    public class ResponseFrontEndLoadMoreArticleModel 
    {
        public string current_article_size { get; set; }
        public List<FrontEndArticleModel> data { get; set; }
    }
}
