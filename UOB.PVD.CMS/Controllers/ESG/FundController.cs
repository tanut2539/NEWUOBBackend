using Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UOB.PVD.Helper;
using UOB.PVD.Model.ESG;

namespace UOB.PVD.CMS.Controllers.ESG
{
    public class EvtController : RootController
    {
        private readonly IConfiguration m_config;
        private AppSetting _appSettings;
        public EvtController(IConfiguration config, IOptions<AppSetting> setting)
        {
            m_config = config;
            _appSettings = setting.Value;
        }

        public IActionResult index(string id)
        {
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            ViewBag.screen = "fund";
            //Send To API
            try
            {
                var service = new HTTPService<Response<FundModel>>(m_config["Uri:WebAPI"]);
                service.Authentication = Request.Cookies["token"];
                service.Path = "fund";
                var response = service.GetAsync(id).Result;
 
                if (response.result.Equals(ConstantData.Success))
                {
                    return View(response.Data);
                }
                ViewBag.error = response.message;
                return View(response.Data);
            }
            catch (Exception e)
            {
                try
                {
                    if (ConvertTo.String(e.InnerException.Message).Equals("401"))
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }
                catch { }
                ViewBag.error = e.Message;
                return View(new FundModel());
            }

        }
        public IActionResult list()
        {
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            ViewBag.screen = "fund";
            return View(FrontEndGetList());
        }

        [HttpPost]
        public Response<BaseModel> Add([FromBody] FundModel body)
        {
            var response = new Response<BaseModel>();
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                response.result = DataFactory.Fail;
                response.StatusCode = 401;
                response.message = "กรุณาเข้าสู่ระบบ";
                return response;
            }

            try
            {
                var service = new HTTPService<Response<BaseModel>>(m_config["Uri:WebAPI"]);
                service.Authentication = Request.Cookies["token"];
                service.Path = "fund/create";
                return service.PostAsync(body).Result;
            }
            catch (Exception e)
            {
                response.result = DataFactory.Fail;
                response.message = e.ToString();
                return response;
            }

        }
        [HttpPost]
        public Response<BaseModel> Edit([FromBody] FundModel body)
        {
            var response = new Response<BaseModel>();
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                response.result = DataFactory.Fail;
                response.StatusCode = 401;
                response.message = "กรุณาเข้าสู่ระบบ";
                return response;
            }

            try
            {
                var service = new HTTPService<Response<BaseModel>>(m_config["Uri:WebAPI"]);
                service.Authentication = Request.Cookies["token"];
                service.Path = $"fund/update/{body.fund_seq}";
                return service.PutAsync(body).Result;
            }
            catch (Exception e)
            {
                response.result = DataFactory.Fail;
                response.message = e.ToString();
                return response;
            }

        }
        [HttpPost]
        public Response<BaseModel> Delete([FromBody] FundModel body)
        {
            var response = new Response<BaseModel>();
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                response.result = DataFactory.Fail;
                response.StatusCode = 401;
                response.message = "กรุณาเข้าสู่ระบบ";
                return response;
            }

            try
            {
                var service = new HTTPService<Response<BaseModel>>(m_config["Uri:WebAPI"]);
                service.Authentication = Request.Cookies["token"];
                service.Path = $"fund/{body.fund_seq}";
                return service.DeleteAsync().Result;
            }
            catch (Exception e)
            {
                response.result = DataFactory.Fail;
                response.message = e.ToString();
                return response;
            }

        }
        [HttpPost]
        public Response<BaseModel> Publish([FromBody] FundModel body)
        {
            var response = new Response<BaseModel>();
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                response.result = DataFactory.Fail;
                response.StatusCode = 401;
                response.message = "กรุณาเข้าสู่ระบบ";
                return response;
            }

            try
            {
                var service = new HTTPService<Response<BaseModel>>(m_config["Uri:WebAPI"]);
                service.Authentication = Request.Cookies["token"];
                service.Path = $"/fund/set-publish/{body.fund_seq}";
                return service.PutAsync(body).Result;
            }
            catch (Exception e)
            {
                response.result = DataFactory.Fail;
                response.message = e.ToString();
                return response;
            }
        }
        public Response<DatataFundResponseModel> GetList(DatatableSearchFund body)
        {
            try
            {
                var service = new HTTPService<Response<DatataFundResponseModel>>(m_config["Uri:WebAPI"]);
                service.Authentication = Request.Cookies["token"];
                service.Path = "fund/getlist";
                return service.PostAsync(body).Result;
            }
            catch (Exception e)
            {
                var error = new Response<DatataFundResponseModel>();
                error.Data = null;
                error.result = ConstantData.Fail;
                error.message = e.Message;
                return error;
            }


        }
        [HttpPost]
        public List<FrontEndFundModel> FrontEndGetList()
        {
            var response = new Response<List<FrontEndFundModel>>();
            try
            {
                var service = new HTTPService<Response<List<FrontEndFundModel>>>(m_config["Uri:WebAPI"]);
                service.Authentication = Request.Cookies["token"];
                service.Path = $"fund/get-list";
                response = service.GetAsync().Result;
                return response.Data;
            }
            catch (Exception e)
            {
                return new List<FrontEndFundModel>();
            }

        }
        [HttpPost]
        public Response<BaseModel> SaveOrderBy([FromBody] FundEditOrderModel body)
        {
            var response = new Response<BaseModel>();
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                response.result = DataFactory.Fail;
                response.StatusCode = 401;
                response.message = "กรุณาเข้าสู่ระบบ";
                return response;
            }

            try
            {
                var service = new HTTPService<Response<BaseModel>>(m_config["Uri:WebAPI"]);
                service.Authentication = Request.Cookies["token"];
                service.Path = $"fund/set-orderby";
                return service.PutAsync(body).Result;
            }
            catch (Exception e)
            {
                response.result = DataFactory.Fail;
                response.message = e.ToString();
                return response;
            }
        }
        public async Task<ResponseImage> UploadFile(IFormFile files)
        {
            string fileNames = "";
            using (var ms = new MemoryStream())
            {
                files.CopyTo(ms);
                var fileBytes = ms.ToArray();
                var bytes = Convert.FromBase64String(Convert.ToBase64String(fileBytes));
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\FileUpload\Fund");
                fileNames = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(files.FileName);  //Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(files.FileName);
                string path = Path.Combine(filePath, fileNames);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await files.CopyToAsync(fileStream);
                }
            }
            ResponseImage res = new ResponseImage();
            res.name = fileNames;
            res.url = string.Format("{0}FileUpload/Fund/{1}", m_config["Uri:CMS"], fileNames);
            return res;
        }

    }
}
