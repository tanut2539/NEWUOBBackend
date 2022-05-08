using Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
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
    public class BannerController : RootController
    {
        private readonly IConfiguration m_config;
        private AppSetting _appSettings;
        public BannerController(IConfiguration config, IOptions<AppSetting> setting)
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

            ViewBag.screen = "banner";
            var requestCultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = requestCultureFeature.RequestCulture.UICulture;

            //Send To API
            try
            {
                var service = new HTTPService<Response<BannerModel>>(m_config["Uri:WebAPI"]);
                service.Authentication = Request.Cookies["token"];
                service.Path = "banner";
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
                return View(new BannerModel());
            }
        }
        public IActionResult list()
        {
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            ViewBag.screen = "banner";
            return View();
        }

        public class langdada
        {
            public string lang { get; set; }
        }
        [HttpPost]
        public BaseResponse SetlLang([FromBody] langdada data)
        {
            BaseResponse response = new BaseResponse();
            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(data.lang));
            var option = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) };
            Response.Cookies.Append("Language", cookieValue, option);
            return response;
        }
        [HttpPost]
        public Response<BaseModel> Add([FromBody] BannerModel body)
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
                service.Path = "banner/create";
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
        public Response<BaseModel> Edit([FromBody] BannerModel body)
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
                service.Path = $"banner/update/{body.banner_seq}";
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
        public Response<BaseModel> Delete([FromBody] BannerModel body)
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
                service.Path = $"banner/{body.banner_seq}";
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
        public Response<BaseModel> Publish([FromBody] BannerModel body)
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
                service.Path = $"banner/set-publish/{body.banner_seq}";
                return service.PutAsync(body).Result;
            }
            catch (Exception e)
            {
                response.result = DataFactory.Fail;
                response.message = e.ToString();
                return response;
            }
        }

        public Response<DatataBannerResponseModel> GetList(DatatableSearchBanner body)
        {
            try
            {
                var service = new HTTPService<Response<DatataBannerResponseModel>>(m_config["Uri:WebAPI"]);
                service.Authentication = Request.Cookies["token"];
                service.Path = "banner/getlist";
                return service.PostAsync(body).Result;
            }
            catch (Exception e)
            {
                var error = new Response<DatataBannerResponseModel>();
                error.Data = null;
                error.result = ConstantData.Fail;
                error.message = e.Message;
                return error;
            }

        }

        [DisableRequestSizeLimit]
        [HttpPost]
        public async Task<ResponseImage> UploadFile(IFormFile files)
        {
            string fileNames = "";
            try
            {
                using (var ms = new MemoryStream())
                {
                    files.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    var bytes = Convert.FromBase64String(Convert.ToBase64String(fileBytes));
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\FileUpload\Banner");
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
                res.url = string.Format("{0}FileUpload/Banner/{1}", m_config["Uri:CMS"], fileNames);
                return res;
            }
            catch (Exception e)
            {
                ResponseImage res = new ResponseImage();
                res.result = "fail";
                res.message = e.Message;
                return res;
            }
        }

    }
}
