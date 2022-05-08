using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UOB.PVD.Helper;
using UOB.PVD.Model.ESG;
using UOB.PVD.Repository.Article;

namespace UOB.PVD.API.Controllers.ESG.CMS
{
    [ApiExplorerSettings(GroupName = "CMS")]
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
        [Authorize]
        [Route("cms-article/{id}")]
        public Response<ArticleModel> Get(string id)
        {
            var response = new Response<ArticleModel>();
            var username = User.Claims.Where(x => x.Type == "username").FirstOrDefault()?.Value;
            try
            {
                DataRow dr = article.GetData(connectionString, id);
                if (dr is not null)
                {
                    response.Data = HelperFunctions.ToObject<ArticleModel>(dr);
                    response.Data.url_image_th = !string.IsNullOrWhiteSpace(response.Data.image_th) ? string.Format("{0}FileUpload/Article/{1}", m_config["Uri:CMS"], response.Data.image_th) : null;
                    response.Data.url_image_en = !string.IsNullOrWhiteSpace(response.Data.image_en) ? string.Format("{0}FileUpload/Article/{1}", m_config["Uri:CMS"], response.Data.image_en) : null;
                    response.Data.url_image_thumbnail_th = !string.IsNullOrWhiteSpace(response.Data.image_thumbnail_th) ? string.Format("{0}FileUpload/Article/{1}",  m_config["Uri:CMS"], response.Data.image_thumbnail_th) : null;
                    response.Data.url_image_thumbnail_en = !string.IsNullOrWhiteSpace(response.Data.image_thumbnail_en) ? string.Format("{0}FileUpload/Article/{1}",  m_config["Uri:CMS"], response.Data.image_thumbnail_en) : null;
                }
                response.result = DataFactory.Fail;
                response.message = "Banner not found";
            }
            catch (Exception e)
            {
                response.result = DataFactory.Fail;
                response.message = e.ToString();
            }
            return response;
        }

        [HttpPost]
        [Authorize]
        [Route("cms-article/getlist")]
        public Response<DatataArticleResponseModel> Post( DatatableSearchArticle body)
        {
            var response = new Response<DatataArticleResponseModel>();
            var results = new DatataBannerResponseModel();
            try
            {
 
                string base_picture_url = string.Format("{0}FileUpload/Article/", m_config["Uri:CMS"]);
                DataTable dt = article.GetAllList(connectionString, base_picture_url, body);
                var data = HelperFunctions.ConvertToList<DatataArticleResultModel>(dt);
                response.Data.data = data;
                response.Data.recordsFiltered = dt.Rows.Count > 0 ? ConvertTo.Int(dt.Rows[0]["maxrow"]) : 0;

                return response;
            }
            catch (Exception e)
            {
                response.result = DataFactory.Fail;
                response.message = e.ToString();
            }
            return response;
        }

        [HttpPost]
        [Authorize]
        [Route("cms-article/create")]
        public Response<BaseModel> Create( ArticleModel body)
        {

            var response = new Response<BaseModel>();
 

            try
            {
                DataRow data = article.SetData(connectionString, body);
                data["status"] = 0;
                data["create_by"] = User.Identity.Name;
                data["create_date"] = DateTime.Now;
                article.Add(connectionString, data);
            }
            catch (Exception e)
            {
                response.result = DataFactory.Fail;
                response.message = e.ToString();
            }

            return response;
        }
        [HttpPut]
        [Authorize]
        [Route("cms-article/update/{id}")]
        public Response<BaseModel> Update(string id, ArticleModel body)
        {
            var response = new Response<BaseModel>();
 
            try
            {
                DataRow data = article.SetData(connectionString, body);
                data["modify_by"] = User.Identity.Name;
                data["modify_date"] = DateTime.Now;
                article.Edit(connectionString, data);
            }
            catch (Exception e)
            {
                response.result = DataFactory.Fail;
                response.message = e.ToString();
            }

            return response;
        }

        [HttpDelete]
        [Authorize]
        [Route("cms-article/{id}")]
        public Response<BaseModel> Delete( string id)
        {
            var response = new Response<BaseModel>();
 
            try
            {
                DataRow dr = article.GetData(connectionString, id);
                if (dr == null)
                {
                    response.result = DataFactory.Fail;
                    response.message = "Article not found";
                }
                article.Delete(connectionString, id);
            }
            catch (Exception e)
            {
                response.result = DataFactory.Fail;
                response.message = e.ToString();
            }

            return response;
        }

        [HttpPut]
        [Authorize]
        [Route("cms-article/set-publish/{id}")]
        public Response<BaseModel> Publish( string id, ArticleModel body)
        {
            var response = new Response<BaseModel>();
 
            try
            {
                DataRow dr = article.GetData(connectionString, id);
                if (dr == null)
                {
                    response.result = DataFactory.Fail;
                    response.message = "Article not found";
                }
                string status = ConvertTo.String(dr["status"]);
                if (status is "0")
                {
                    try
                    {
                        DataRow data = article.SetData(connectionString, body);
                        data["modify_by"] = User.Identity.Name;
                        data["modify_date"] = DateTime.Now;
                        article.Edit(connectionString, data);
                    }
                    catch
                    {
                        response.result = DataFactory.Fail;
                        response.message = "ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบ";
                    }

                    article.Publish(connectionString, body.article_seq, User.Identity.Name);//publish
                }
                else
                {
                    article.UnPublish(connectionString, body.article_seq, User.Identity.Name);//un publish
                }
                return response;
            }
            catch (Exception e)
            {
                response.result = DataFactory.Fail;
                response.message = e.ToString();
            }
            return response;
        }

    }
}
