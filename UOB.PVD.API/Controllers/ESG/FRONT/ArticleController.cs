using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UOB.PVD.Helper;
using UOB.PVD.Model.ESG;
using UOB.PVD.Model.ESG.FrontEnd;
using UOB.PVD.Repository.Article;

namespace UOB.PVD.API.Controllers.ESG.FRONT
{
    [ApiExplorerSettings(GroupName = "FrontEnd")]
    public class ArticleController : RootController
    {
        string connectionString = "";
        private readonly IConfiguration m_config;
        private AppSetting _appSettings;
        public ArticleController(IConfiguration config, IOptions<AppSetting> setting)
        {
            m_config = config;
            connectionString = m_config.GetConnectionString("ESGConnection");
            _appSettings = setting.Value;
        }

        private article _article;
        private article article
        {
            set
            {
                _article = value;
            }
            get
            {
                if (_article == null)
                {
                    _article = new article();
                }
                return _article;
            }
        }

        [HttpGet]
        [Route("article/{id}")]
        public Response<FrontEndArticleDetailModel> Get(string id)
        {
            var Response = new Response<FrontEndArticleDetailModel>();
            Request.Headers.TryGetValue("lang", out var culture);

            try
            {
                DataRow Article = ConvertTo.String(culture).Equals("th-TH")
                           ? article.FrontEndGetDataTH(connectionString, id)
                           : article.FrontEndGetDataEN(connectionString, id);
                if (Article is not null)
                {
                    FrontEndArticleDetailModel ViewModel = HelperFunctions.ToObject<FrontEndArticleDetailModel>(Article);
                    ViewModel.str_publish_date = ConvertTo.StringThaiDate(ViewModel.publish_date);
                    ViewModel.thisURL = string.Format("{0}article/{1}", _appSettings.FRONT_URL, id);
                    Response.Data = ViewModel;
                    return Response;
                }
                Response.result = ConstantData.NotFound;
                Response.message = ConstantData.NotFound;
            }
            catch (Exception e)
            {
                Response.result = ConstantData.Fail;
                Response.message = e.Message;
            }
            return Response;
        }

        [HttpPost]
        [Route("article/getlist")]
        public Response<ResponseFrontEndLoadMoreArticleModel> Get(FrontEndLoadMoreArticleModel body)
        {
            var Respons = new Response<ResponseFrontEndLoadMoreArticleModel>();
            int start = 3;
            int end = 6;
            if (!string.IsNullOrEmpty(body.current_article_size))
            {
                start = ConvertTo.Int(body.current_article_size);
                end = start + 3;
            }

            try
            {
                DataTable dt = null;
                Request.Headers.TryGetValue("lang", out var culture);
                if (ConvertTo.String(culture) == "th-TH")
                {
                    dt = article.FrontEndGetAllListTH(connectionString, _appSettings.CMS_URL + "FileUpload/Article/", start, end);
                }
                else
                {
                    dt = article.FrontEndGetAllListTH(connectionString, _appSettings.CMS_URL + "FileUpload/Article/", start, end);
                }

                ResponseFrontEndLoadMoreArticleModel response = new ResponseFrontEndLoadMoreArticleModel();
                response.current_article_size = ConvertTo.String(start + ConvertTo.Int(dt.Rows.Count));
                response.data = HelperFunctions.ConvertToList<FrontEndArticleModel>(dt);
                Respons.Data = response;
            }
            catch (Exception e)
            {
                Respons.result = ConstantData.Fail;
                Respons.message = e.Message;
            }

            return Respons;

        }
    }
}
