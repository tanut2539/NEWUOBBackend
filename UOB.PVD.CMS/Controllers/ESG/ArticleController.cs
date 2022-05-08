using Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UOB.PVD.CMS.Controllers;
using UOB.PVD.Helper;
using UOB.PVD.Model.ESG;

namespace CMS.Controllers
{
    public class ArticleController : RootController
    {
 
        private readonly IConfiguration m_config;
        private AppSetting _appSettings;
        public ArticleController(IConfiguration config, IOptions<AppSetting> setting)
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
            ViewBag.screen = "article";
            //Send To API
            try
            {
                var service = new HTTPService<Response<ArticleModel>>(m_config["Uri:WebAPI"]);
                service.Authentication = Request.Cookies["token"];
                service.Path = "cms-article";
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
                return View(new ArticleModel());
            }
        }
        public IActionResult list()
        {
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            ViewBag.screen = "article";
            return View();
        }

        [HttpPost]
        public Response<BaseModel> Add([FromBody] ArticleModel body)
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
                service.Path = "cms-article/create";
                return service.PostAsync(body).Result;
            }
            catch (Exception e)
            {
                try
                {
                    if (ConvertTo.String(e.InnerException.Message).Equals("401"))
                    {
                        response.result = DataFactory.Fail;
                        response.StatusCode = 401;
                        response.message = "กรุณาเข้าสู่ระบบ";
                        return response;
                    }
                }
                catch { }
           
                response.result = DataFactory.Fail;
                response.message = e.ToString();
                return response;
            }
        }
        [HttpPost]
        public Response<BaseModel> Edit([FromBody] ArticleModel body)
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
                service.Path = $"cms-article/update/{body.article_seq}";
                return service.PutAsync(body).Result;
            }
            catch (Exception e)
            {
                try
                {
                    if (ConvertTo.String(e.InnerException.Message).Equals("401"))
                    {
                        response.result = DataFactory.Fail;
                        response.StatusCode = 401;
                        response.message = "กรุณาเข้าสู่ระบบ";
                        return response;
                    }
                }
                catch { }
                response.result = DataFactory.Fail;
                response.message = e.ToString();
                return response;
            }
        }
        [HttpPost]
        public Response<BaseModel> Delete([FromBody] ArticleModel body)
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
                service.Path = $"cms-article/{body.article_seq}";
                return service.DeleteAsync().Result;
            }
            catch (Exception e)
            {
                try
                {
                    if (ConvertTo.String(e.InnerException.Message).Equals("401"))
                    {
                        response.result = DataFactory.Fail;
                        response.StatusCode = 401;
                        response.message = "กรุณาเข้าสู่ระบบ";
                        return response;
                    }
                }
                catch { }
                response.result = DataFactory.Fail;
                response.message = e.ToString();
                return response;
            }

        }
        [HttpPost]
        public Response<BaseModel> Publish([FromBody] ArticleModel body)
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
                service.Path = $"/cms-article/set-publish/{body.article_seq}";
                return service.PutAsync(body).Result;
            }
            catch (Exception e)
            {
                try
                {
                    if (ConvertTo.String(e.InnerException.Message).Equals("401"))
                    {
                        response.result = DataFactory.Fail;
                        response.StatusCode = 401;
                        response.message = "กรุณาเข้าสู่ระบบ";
                        return response;
                    }
                }
                catch { }
                response.result = DataFactory.Fail;
                response.message = e.ToString();
                return response;
            }
        }

        public Response<DatataArticleResponseModel> GetList(DatatableSearchArticle body)
        {
            try
            {
                var service = new HTTPService<Response<DatataArticleResponseModel>>(m_config["Uri:WebAPI"]);
                service.Authentication = ConvertTo.String(Request.Cookies["token"]);
                service.Path = "cms-article/getlist";
                return service.PostAsync(body).Result;
            }
            catch (Exception e)
            {
                var error = new Response<DatataArticleResponseModel>();
                error.Data = null;
                error.result = ConstantData.Fail;
                error.message = e.Message;
                return error;
            }

        }

        [HttpPost]
        public async Task<ResponseImage> UploadFile(IFormFile files)
        {
            string fileNames = "";
            using (var ms = new MemoryStream())
            {
                files.CopyTo(ms);
                var fileBytes = ms.ToArray();
                var bytes = Convert.FromBase64String(Convert.ToBase64String(fileBytes));
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\FileUpload\Article");
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
            res.url = string.Format("{0}FileUpload/Article/{1}", m_config["Uri:CMS"], fileNames);
            return res;
        }

    }
}
