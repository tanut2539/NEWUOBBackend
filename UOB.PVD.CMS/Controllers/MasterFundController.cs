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
    public class MasterFundController : RootController
    {
        // 2. กองทุนสำรองเลี้ยงชีพ ยูโอบี มาสเตอร์ ฟันด์ ซึ่งจดทะเบียนแล้ว (UOB Master Fund)
        //fund_seq = 2
        private readonly IConfiguration _configuration;
        public MasterFundController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> InCountryAsync(string id, string policy_type)
        {
            //การลงทุนในประเทศ
            //investment_type_id = IN
            ViewBag.investment_type = "IN";
            ViewBag.system = "MF";
            ViewBag.screen = "MF.InCountry";
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }

            var ViewModel = new MasterFundDTO();
            if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(policy_type))
            {
                try
                {
                    var service = new HTTPService(_configuration["Uri:WebAPI"]);
                    service.Authentication = ConvertTo.String(Request.Cookies["token"]);
                    service.Path = "investor-choice";
                    var result = service.GetAsync($"{id}/{policy_type}");
                    if (result.StatusCode is HttpStatusCode.Unauthorized)
                    {
                        return RedirectToLogin();
                    }
                    else
                    {
                        string contents = await result.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<Response<MasterFundDTO>>(contents);
                        ViewModel = response.Data;
                    }
                }
                catch
                {
                    return View(ViewModel);
                }
            }
            else
            {
                return View(ViewModel);
            }
            return View(ViewModel);
        }
        public async Task<IActionResult> InCountryListAsync()
        {
            ViewBag.system = "MF";
            ViewBag.screen = "MF.InCountry";
            var response = new List<MasterFundListDTO>();
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            var search = new MasterFundSearchListDTO();
            search.InvestorPage = "IN";

            var service = new HTTPService(_configuration["Uri:WebAPI"]);
            service.Path = "master-fund/list";
            service.Authentication = ConvertTo.String(Request.Cookies["token"]);
            var result = service.PostAsync(search);
            if (result.StatusCode is HttpStatusCode.Unauthorized)
            {
                return RedirectToLogin();
            }
            else
            {
                string contents = await result.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<Response<List<MasterFundListDTO>>>(contents).Data;
            }

            return View(response);
        }
        [HttpPost]
        public async Task<Response<BaseModel>> CreateAsync([FromBody] MasterFundDTO dto)
        {
            var response = new Response<BaseModel>();
            try
            {
                //Send To API

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "master-fund/create";
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
        public async Task<Response<BaseModel>> UpdateAsync([FromBody] MasterFundDTO dto)
        {
            var response = new Response<BaseModel>();
            try
            {
                //Send To API

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "master-fund/update";
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
        public async Task<Response<BaseModel>> DeleteAsync([FromBody] MasterFundDTO dto)
        {
            var response = new Response<BaseModel>();
            try
            {
                //Send To API

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "master-fund/delete";
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

        public async Task<IActionResult> OutCountryAsync(string id, string policy_type)
        {
            //การลงทุนต่างประเทศ
            //investment_type_id = OUT
            ViewBag.investment_type = "OUT";
            ViewBag.system = "MF";
            ViewBag.screen = "MF.OutCountry";
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }

            var ViewModel = new MasterFundDTO();
            if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(policy_type))
            {
                try
                {
                    var service = new HTTPService(_configuration["Uri:WebAPI"]);
                    service.Authentication = ConvertTo.String(Request.Cookies["token"]);
                    service.Path = "master-fund";
                    var result = service.GetAsync($"{id}/{policy_type}");
                    if (result.StatusCode is HttpStatusCode.Unauthorized)
                    {
                        return RedirectToLogin();
                    }
                    else
                    {
                        string contents = await result.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<Response<MasterFundDTO>>(contents);
                        ViewModel = response.Data;
                    }
                }
                catch
                {
                    return View(ViewModel);
                }
            }
            else
            {
                return View(ViewModel);
            }
            return View(ViewModel);
        }
        public async Task<IActionResult> OutCountryListAsync()
        {
            ViewBag.system = "MF";
            ViewBag.screen = "MF.OutCountry";
            var response = new List<MasterFundListDTO>();
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            var search = new MasterFundSearchListDTO();
            search.InvestorPage = "OUT";

            var service = new HTTPService(_configuration["Uri:WebAPI"]);
            service.Path = "master-fund/list";
            service.Authentication = ConvertTo.String(Request.Cookies["token"]);
            var result = service.PostAsync(search);
            if (result.StatusCode is HttpStatusCode.Unauthorized)
            {
                return RedirectToLogin();
            }
            else
            {
                string contents = await result.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<Response<List<MasterFundListDTO>>>(contents).Data;
            }

            return View(response);
        }

        [HttpPost]
        public async Task<ResponseFile> UploadFile(List<IFormFile> files, string investment_type, string language)
        {
            ResponseFile response = new ResponseFile();
            string uploadPath = $@"\file\pdf";
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
