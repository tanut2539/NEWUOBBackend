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
    public class InvestorChoiceController : RootController
    {
        //กองทุนสำรองเลี้ยงชีพยูโอบี อินเวสเตอร์ ชอยส์ ซึ่งจดทะเบียนแล้ว (UOB Investor Choice)
        //fund_seq = 1
        private readonly IConfiguration _configuration;
        public InvestorChoiceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
 
        public async Task<IActionResult> InCountryAsync(string id,string policy_type)
        {
            //การลงทุนในประเทศ
            //investment_type_id = IN
            ViewBag.investment_type = "IN";
            ViewBag.system = "IC";
            ViewBag.screen = "IC.InCountry";
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }

            var ViewModel = new InvestorChoiceDTO();
            if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(policy_type))
            { 
                try
                {
                    var service = new HTTPService(_configuration["Uri:WebAPI"]);
                    service.Authentication = ConvertTo.String(Request.Cookies["token"]);
                    service.Path = "investor-choice";
                    var result = service.GetAsync($"{id}/{policy_type}" );
                    if (result.StatusCode is HttpStatusCode.Unauthorized)
                    {
                        return RedirectToLogin();
                    }
                    else
                    {
                        string contents = await result.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<Response<InvestorChoiceDTO>>(contents);
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
            ViewBag.system = "IC";
            ViewBag.screen = "IC.InCountry";
            var response = new List<InvestorChoiceListDTO>();
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            var search = new InvestorChoiceSearchListDTO();
            search.InvestorPage = "IN";

            try
            {
                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "investor-choice/list";
                service.Authentication = ConvertTo.String(Request.Cookies["token"]);
                var result = service.PostAsync(search);
                if (result.StatusCode is HttpStatusCode.Unauthorized)
                {
                    return RedirectToLogin();
                }
                else
                {
                    string contents = await result.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<Response<List<InvestorChoiceListDTO>>>(contents).Data;
                }
            }
            catch{

            }


            return View(response);
        }
        public async Task<IActionResult> ManageChart()
        {
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            var ViewModel = new List<ChartFileDTO>();
            ChartFileDTO dto = new ChartFileDTO();
 
            var service = new HTTPService(_configuration["Uri:WebAPI"]);
            service.Path = "chart-file/getlist";
            service.Authentication = ConvertTo.String(Request.Cookies["token"]);
            var result = service.PostAsync(dto);
            if (result.StatusCode is HttpStatusCode.Unauthorized)
            {
                return RedirectToLogin();
            }
            else
            {
                ViewBag.system = "IC";
                ViewBag.screen = "IC.ManageChart";
                string contents = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Response<List<ChartFileDTO>>>(contents);
                ViewModel = response.Data;
                return View(ViewModel);
            }
        }

        public async Task<IActionResult> OutCountryAsync(string id, string policy_type)
        {
            ViewBag.system = "IC";
            ViewBag.screen = "IC.OutCountry";
            ViewBag.investment_type = "OUT";
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            var ViewModel = new InvestorChoiceDTO();
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
                        var response = JsonConvert.DeserializeObject<Response<InvestorChoiceDTO>>(contents);
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
            ViewBag.system = "IC";
            ViewBag.screen = "IC.OutCountry";
            var response = new List<InvestorChoiceListDTO>();
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            var search = new InvestorChoiceSearchListDTO();
            search.InvestorPage = "OUT";

            var service = new HTTPService(_configuration["Uri:WebAPI"]);
            service.Path = "investor-choice/list";
            service.Authentication = ConvertTo.String(Request.Cookies["token"]);
            var result = service.PostAsync(search);
            if (result.StatusCode is HttpStatusCode.Unauthorized)
            {
                return RedirectToLogin();
            }
            else
            {
                string contents = await result.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<Response<List<InvestorChoiceListDTO>>>(contents).Data;
            }
            return View(response);
        }


        [HttpPost]
        public async Task<Response<BaseModel>> CreateAsync([FromBody] InvestorChoiceDTO dto)
        {
            var response = new Response<BaseModel>();
            try
            {
                //Send To API

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "investor-choice/create";
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
        public async Task<Response<BaseModel>> UpdateAsync([FromBody] InvestorChoiceDTO dto)
        {
            var response = new Response<BaseModel>();
            try
            {
                //Send To API

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "investor-choice/update";
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
        public async Task<Response<BaseModel>> DeleteAsync([FromBody] InvestorChoiceDTO dto)
        {
            var response = new Response<BaseModel>();
            try
            {
                //Send To API

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "investor-choice/delete";
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
        public async Task<ResponseFile> UploadFile(List<IFormFile> files, string investment_type,string language)
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

                    response.url = string.Format("{0}{1}/{2}", _configuration["Uri:CMS"], uploadPath.Replace("\\", "/") , fileNames);
                    response.name = fileNames;
                    response.FileOriginalName = file.FileName;
                }

                return response;
            }
        }

        [HttpPost]
        public async Task<Response<BaseModel>> ChartCreateAsync([FromBody] ChartFileDTO dto)
        {
            var response = new Response<BaseModel>();
            try
            {
                //Send To API

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "chart-file/create";
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
        public async Task<Response<BaseModel>> ChartUpdateAsync([FromBody] ChartFileDTO dto)
        {
            var response = new Response<BaseModel>();
            try
            {
                //Send To API

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "chart-file/update";
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
        public async Task<Response<BaseModel>> ChartDeleteAsync([FromBody] ChartFileDTO dto)
        {
            var response = new Response<BaseModel>();
            try
            {
                //Send To API

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "chart-file/delete";
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
        public async Task<Response<ChartFileDTO>> ChartGetDetailAsync([FromBody] ChartFileDTO dto)
        {
            var response = new Response<ChartFileDTO>();
            try
            {
                //Send To API

                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "chart-file/detail";
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
                    response = JsonConvert.DeserializeObject<Response<ChartFileDTO>>(contents);
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
        public async Task<ResponseFile> UploadChartFile(List<IFormFile> files )
        {
            ResponseFile response = new ResponseFile();
            string uploadPath = $@"\file\chart";
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
