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
using UOB.PVD.Repository.Banner;

namespace UOB.PVD.API.Controllers.ESG.CMS
{
    [ApiExplorerSettings(GroupName = "CMS")]
    public class BannerController : RootController
    {
        string connectionString = "";
        private readonly IConfiguration m_config;
        private AppSetting _appSettings;

        //interface helper


        public BannerController(IConfiguration config, IOptions<AppSetting> setting )
        {
            m_config = config;
            connectionString = m_config.GetConnectionString("ESGConnection");
            _appSettings = setting.Value;
        }
 
        private banner _banner;
        private banner banner
        {
            set
            {
                _banner = value;
            }
            get
            {
                if (_banner == null)
                {
                    _banner = new banner();
                }
                return _banner;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("banner/{id}")]
        public Response<BannerModel> Get(string id)
        {
            var response = new Response<BannerModel>();
 
            try
            {
                DataRow dr = banner.GetData(connectionString, id);
                if (dr is not null)
                {
                    response.Data = HelperFunctions.ToObject<BannerModel>(dr);
                    response.Data.url_image_th = !string.IsNullOrWhiteSpace(response.Data.image_th) ? string.Format("{0}FileUpload/Banner/{1}", m_config["Uri:CMS"], response.Data.image_th) : null;
                    response.Data.url_image_en = !string.IsNullOrWhiteSpace(response.Data.image_en) ? string.Format("{0}FileUpload/Banner/{1}", m_config["Uri:CMS"], response.Data.image_en) : null;
                    response.Data.url_image_mobile_th = !string.IsNullOrWhiteSpace(response.Data.image_mobile_th) ? string.Format("{0}FileUpload/Banner/{1}", m_config["Uri:CMS"], response.Data.image_mobile_th) : null;
                    response.Data.url_image_mobile_en = !string.IsNullOrWhiteSpace(response.Data.image_mobile_en) ? string.Format("{0}FileUpload/Banner/{1}", m_config["Uri:CMS"], response.Data.image_mobile_en) : null;
                    response.Data.url_vdo = !string.IsNullOrWhiteSpace(response.Data.vdo) ? string.Format("{0}FileUpload/Banner/{1}", m_config["Uri:CMS"], response.Data.vdo) : null;
                    return response;
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
        [Route("banner/getlist")]
        public Response<DatataBannerResponseModel> Post(  DatatableSearchBanner body)
        {
            var response = new Response<DatataBannerResponseModel>();
            var results = new DatataBannerResponseModel();
            try
            {
 

                string base_picture_url = string.Format("{0}FileUpload/Banner/", m_config["Uri:CMS"]);
                DataTable dt = banner.GetAllList(connectionString, base_picture_url, body);
                List<DatataBannerResultModel> data = HelperFunctions.ConvertToList<DatataBannerResultModel>(dt);
                results.data = data;
                results.recordsFiltered = dt.Rows.Count > 0 ? ConvertTo.Int(dt.Rows[0]["maxrow"]) : 0;
                response.Data = results;
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
        [Route("banner/create")]
        public Response<BaseModel> Create(  BannerModel body)
        {

            var response = new Response<BaseModel>();
 
            try
            {
                DataRow data = banner.SetData(connectionString, body);
                data["status"] = 0;
                data["create_by"] = User.Claims.Where(x => x.Type == "username").FirstOrDefault()?.Value;
                data["create_date"] = DateTime.Now;
                banner.Add(connectionString, data);
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
        [Route("banner/update/{id}")]
        public Response<BaseModel> Update( string id, BannerModel body)
        {
            var response = new Response<BaseModel>();
 

            try
            {
                DataRow data = banner.SetData(connectionString, body);
                data["modify_by"] = User.Claims.Where(x => x.Type == "username").FirstOrDefault()?.Value;
                data["modify_date"] = DateTime.Now;
                banner.Edit(connectionString, data);
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
        [Route("banner/{id}")]
        public Response<BaseModel> Delete( string id)
        {
            var response = new Response<BaseModel>();
 
            try
            {
                DataRow dr = banner.GetData(connectionString, id);
                if (dr == null)
                {
                    response.result = DataFactory.Fail;
                    response.message = "Banner not found";
                    return response;
                }
                banner.Delete(connectionString, id);
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
        [Route("banner/set-publish/{id}")]
        public Response<BaseModel> Publish( string id, BannerModel body)
        {
            var response = new Response<BaseModel>();
            var username = User.Claims.Where(x => x.Type == "username").FirstOrDefault()?.Value; 
            try
            {
                DataRow dr = banner.GetData(connectionString, id);
                if (dr == null)
                {
                    response.result = DataFactory.Fail;
                    response.message = "Banner not found";
                }
                string status = ConvertTo.String(dr["status"]);
                if (status is "0")
                {
                    try
                    {
                        DataRow data = banner.SetData(connectionString, body);
                        data["modify_by"] = username;
                        data["modify_date"] = DateTime.Now;
                        banner.Edit(connectionString, data);
                    }
                    catch
                    {
                        response.result = DataFactory.Fail;
                        response.message = "ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบ";
                    }

                    banner.Publish(connectionString, body.banner_seq, username);//publish
                }
                else
                {
                    banner.UnPublish(connectionString, body.banner_seq, username);//un publish
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
