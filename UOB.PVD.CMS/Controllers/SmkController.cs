using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UOB.PVD.Helper;
using UOB.PVD.Model;

namespace UOB.PVD.CMS.Controllers
{
    public class SmkController : RootController
    {
        private readonly IConfiguration _configuration;
        public SmkController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> smk1Async()
        {
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            ViewBag.system = "SMK1";
            ViewBag.screen = "SMK1";
            var ViewModel = new List<SmkFileDTO>();
            SmkFileDTO dto = new SmkFileDTO();
            dto.smk_type = "SMK1";
            var service = new HTTPService(_configuration["Uri:WebAPI"]);
            service.Path = "smk/getlist";
            service.Authentication = ConvertTo.String(Request.Cookies["token"]);
            var result = service.PostAsync(dto);
            if (result.StatusCode is HttpStatusCode.Unauthorized)
            {
                return RedirectToLogin();
            }
            else
            {
                string contents = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Response<List<SmkFileDTO>>>(contents);
                ViewModel = response.Data;
                return View(ViewModel);
            }
 
        }
        public async Task<IActionResult> smk2Async()
        {
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            ViewBag.system = "SMK2";
            ViewBag.screen = "SMK2";
            var ViewModel = new List<SmkFileDTO>();
            SmkFileDTO dto = new SmkFileDTO();
            dto.smk_type = "SMK2";
            var service = new HTTPService(_configuration["Uri:WebAPI"]);
            service.Path = "smk/getlist";
            service.Authentication = ConvertTo.String(Request.Cookies["token"]);
            var result = service.PostAsync(dto);
            if (result.StatusCode is HttpStatusCode.Unauthorized)
            {
                return RedirectToLogin();
            }
            else
            {
                string contents = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Response<List<SmkFileDTO>>>(contents);
                ViewModel = response.Data;
                return View(ViewModel);
            }
        }

        [HttpPost]
        public async Task<Response<BaseModel>> CreateAsync([FromBody] SmkFileDTO dto)
        {
            var response = new Response<BaseModel>();
            try
            {
                //Send To API

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "smk/create";
                service.Authentication = ConvertTo.String(Request.Cookies["token"]);
                var result = service.PostAsync(dto);
                if (result.StatusCode is HttpStatusCode.Unauthorized)
                {
                    response.result = ConstantData.Fail;
                    response.message = ConstantData.Status401;
                }
                else
                {
                    string contents = await result.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<Response<BaseModel>>(contents);
                }
                return response;
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return response;
            }
        }
        [HttpPost]
        public async Task<Response<BaseModel>> UpdateAsync([FromBody] SmkFileDTO dto)
        {
            var response = new Response<BaseModel>();
            try
            {
                //Send To API

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "smk/update";
                service.Authentication = ConvertTo.String(Request.Cookies["token"]);
                var result = service.PostAsync(dto);
                if (result.StatusCode is HttpStatusCode.Unauthorized)
                {
                    response.result = ConstantData.Fail;
                    response.message = ConstantData.Status401;
                }
                else
                {
                    string contents = await result.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<Response<BaseModel>>(contents);
                }
                return response;
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return response;
            }
        }
        [HttpPost]
        public async Task<Response<BaseModel>> DeleteAsync([FromBody] SmkFileDTO dto)
        {
            var response = new Response<BaseModel>();
            try
            {
                //Send To API

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "smk/delete";
                service.Authentication = ConvertTo.String(Request.Cookies["token"]);
                var result = service.PostAsync(dto);
                if (result.StatusCode is HttpStatusCode.Unauthorized)
                {
                    response.result = ConstantData.Fail;
                    response.message = ConstantData.Status401;
                }
                else
                {
                    string contents = await result.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<Response<BaseModel>>(contents);
                }
                return response;
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return response;
            }
        }
        [HttpPost]
        public async Task<Response<SmkFileDTO>> GetDetailAsync([FromBody] SmkFileDTO dto)
        {
            var response = new Response<SmkFileDTO>();
            try
            {
                //Send To API

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "smk/detail";
                service.Authentication = ConvertTo.String(Request.Cookies["token"]);
                var result = service.PostAsync(dto);
                if (result.StatusCode is HttpStatusCode.Unauthorized)
                {
                    response.result = ConstantData.Fail;
                    response.message = ConstantData.Status401;
                }
                else
                {
                    string contents = await result.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<Response<SmkFileDTO>>(contents);
                }
                return response;
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return response;
            }
        }

        [HttpPost]
        public async Task<ResponseFile> UploadFile(List<IFormFile> files, string language)
        {
            ResponseFile response = new ResponseFile();
            string uploadPath = $@"\file\smk";
            string fileNames = "";
            using (var ms = new MemoryStream())
            {
                foreach (var file in files)
                {
                    if (!file.ContentType.Contains("application/pdf"))
                    {
                        response.result = "Fail";
                        response.message = "กรุณาอัพโหลดฟล์ PDF เท่านั้น";
                        return response;
                    }
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    var bytes = Convert.FromBase64String(Convert.ToBase64String(fileBytes));
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{uploadPath}");
                    fileNames = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(file.FileName);

                    string path = Path.Combine(filePath, fileNames);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    response.url = string.Format("{0}{1}/{2}", _configuration["Uri:CMS"], uploadPath.Replace("\\", "/"), fileNames);
                    response.name = fileNames;
                    response.FileOriginalName = file.FileName;
                }

                return response;
            }
        }
    }
}
