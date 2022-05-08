using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UOB.PVD.Helper;
using UOB.PVD.Model.ESG;
using UOB.PVD.Repository.Fund;

namespace UOB.PVD.API.Controllers.ESG.CMS
{
    [ApiExplorerSettings(GroupName = "CMS")]
    public class FundController : RootController
    {
        string connectionString = "";
        private readonly IConfiguration m_config;
        private AppSetting _appSettings;

        //interface helper


        public FundController(IConfiguration config, IOptions<AppSetting> setting )
        {
            m_config = config;
            connectionString = m_config.GetConnectionString("ESGConnection");
            _appSettings = setting.Value; 
        }
 

        private fund _fund;
        private fund fund
        {
            set
            {
                _fund = value;
            }
            get
            {
                if (_fund == null)
                {
                    _fund = new fund();
                }
                return _fund;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("fund/{id}")]
        public Response<FundModel> Get(string id)
        {
            var response = new Response<FundModel>();
 
            try
            {
                DataRow dr = fund.GetData(connectionString, id);
                if (dr is not null)
                {
                    response.Data = HelperFunctions.ToObject<FundModel>(dr);
                    response.Data.url_image_th = !string.IsNullOrWhiteSpace(response.Data.image_th) ? string.Format("{0}FileUpload/Fund/{1}",  m_config["Uri:CMS"], response.Data.image_th) : null;
                    response.Data.url_image_en = !string.IsNullOrWhiteSpace(response.Data.image_en) ? string.Format("{0}FileUpload/Fund/{1}",  m_config["Uri:CMS"], response.Data.image_en) : null;

                    return response;
                }
                response.result = DataFactory.Fail;
                response.message = "Fund not found";
            }
            catch (Exception e)
            {
                response.result = DataFactory.Fail;
                response.message = e.ToString();
            }

            return response;
        }
        [HttpGet]
        [Authorize]
        [Route("fund/get-list")]
        public Response<List<FrontEndFundModel>> Get()
        {
            var response = new Response<List<FrontEndFundModel>>();
 
            try
            {
                DataTable Front = fund.FrontEndGetListTH(connectionString,  m_config["Uri:CMS"] + "FileUpload/Fund/");
                response.Data = HelperFunctions.ConvertToList<FrontEndFundModel>(Front);
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
        [Route("fund/getlist")]
        public Response<DatataFundResponseModel> Post(DatatableSearchFund body)
        {
            var response = new Response<DatataFundResponseModel>();

            try
            {
                string base_picture_url = string.Format("{0}FileUpload/Fund/",  m_config["Uri:CMS"]);
                DataTable dt = fund.GetAllList(connectionString, base_picture_url, body);
                var data = HelperFunctions.ConvertToList<DatataFundResultModel>(dt);
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
        [Route("fund/create")]
        public Response<BaseModel> Create(FundModel body)
        {

            var response = new Response<BaseModel>();
 
            try
            {
                DataRow data = fund.SetData(connectionString, body);
                data["status"] = 1;
                data["create_by"] = User.Claims.Where(x => x.Type == "username").FirstOrDefault()?.Value;
                data["create_date"] = DateTime.Now;
                data["publish_date"] = DateTime.Now;
                data["publish_by"] = User.Claims.Where(x => x.Type == "username").FirstOrDefault()?.Value;
                fund.Add(connectionString, data);

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
        [Route("fund/update/{id}")]
        public Response<BaseModel> Update(string id, FundModel body)
        {
            var response = new Response<BaseModel>();
 
            try
            {
                DataRow data = fund.SetData(connectionString, body);
                data["modify_by"] = "";
                data["modify_date"] = DateTime.Now;
                fund.Edit(connectionString, data);
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
        [Route("fund/{id}")]
        public Response<BaseModel> Delete( string id)
        {
            var response = new Response<BaseModel>();
  
            try
            {
                DataRow dr = fund.GetData(connectionString, id);
                if (dr == null)
                {
                    response.result = DataFactory.Fail;
                    response.message = "fund not found";
                    return response;
                }
                fund.Delete(connectionString, id);
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
        [Route("fund/set-publish/{id}")]
        public Response<BaseModel> Publish( string id, FundModel body)
        {
            var response = new Response<BaseModel>();
 

            try
            {
                DataRow dr = fund.GetData(connectionString, id);
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
                        DataRow data = fund.SetData(connectionString, body);
                        data["modify_by"] = User.Claims.Where(x => x.Type == "username").FirstOrDefault()?.Value;
                        data["modify_date"] = DateTime.Now;
                        fund.Edit(connectionString, data);
                    }
                    catch
                    {
                        response.result = DataFactory.Fail;
                        response.message = "ไม่สามารถบันทึกข้อมูลได้ กรุณาตรวจสอบ";
                    }

                    fund.Publish(connectionString, body.fund_seq, User.Claims.Where(x => x.Type == "username").FirstOrDefault()?.Value);//publish
                }
                else
                {
                    fund.UnPublish(connectionString, body.fund_seq, User.Claims.Where(x => x.Type == "username").FirstOrDefault()?.Value);//un publish
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

        [HttpPut]
        [Authorize]
        [Route("fund/set-orderby")]
        public Response<BaseModel> SetOrderBy( FundEditOrderModel body)
        {
            var response = new Response<BaseModel>();
 
            try
            {
                for (var i = 1; i <= body.fund_seq.Count; i++)
                {
                    fund.EditOrderBy(connectionString, i, Convert.ToInt32(body.fund_seq[i]));
                }
                return response;
            }
            catch (Exception e)
            {
                response.result = DataFactory.Fail;
                response.message = e.Message;
            }
            return response;
        }
    }
}
