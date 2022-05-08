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
using System.Text;
using System.Threading.Tasks;
using UOB.PVD.Helper;
using UOB.PVD.Model;
using UOB.PVD.Model.EVT;

namespace UOB.PVD.CMS.Controllers.EVT
{
    public class EvtController : RootController
    {
 
        private readonly IConfiguration _configuration;
        public EvtController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult calendar(string id)
        {
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            ViewBag.screen = "fund";
            var viewModel = new CmsCalendarResponseModel();
            //Send To API
            try
            {
                var service = new HTTPService<Response<CmsCalendarResponseModel>>(_configuration["Uri:WebAPI"]);
                service.Authentication = Request.Cookies["token"];
                service.Path = "evt/calendar";
                viewModel = service.GetAsync().Result.Data;

                return View(viewModel);
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
                return View(new EventModel());
            }

        }

        public IActionResult evevt(string thaiDate , string eventId)
        {
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            ViewBag.screen = "fund";
            //Send To API
            try
            {


                return View(new EventModel());
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
                return View(new EventModel());
            }

        }

        public IActionResult Event(string date, string eventId)
        {
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                return RedirectToLogin();
            }
            var viewModel = new EventModel();
            ViewBag.screen = "fund";
            //Send To API
            try
            {
                String[] arrDate = date.Split('/');

                viewModel.event_date_from = new DateTime(
                    ConvertTo.Int(arrDate[2]),
                    ConvertTo.Int(arrDate[1]),
                    ConvertTo.Int(arrDate[0])).ToString("yyyy-MM-ddTHH:mm:ss");
                viewModel.event_date_to = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                viewModel.register_schedule_from = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                viewModel.register_schedule_to = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                if (!string.IsNullOrWhiteSpace(eventId))
                {

                }
 

                return View(viewModel);
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
                return View(new EventModel());
            }

        }


        [HttpPost]
        public Response<BaseModel> AddEvt([FromBody] EventModel body)
        {
            var response = new Model.Response<Model.BaseModel>();
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                response.result = DataFactory.Fail;
                response.statusCode = 401;
                response.message = "กรุณาเข้าสู่ระบบ";
                return response;
            }

            try
            {
                var errorMsg = ValidateData(body);
                if (!string.IsNullOrWhiteSpace(errorMsg))
                {
                    response.result = DataFactory.Fail;
                    response.message = errorMsg;
                    return response;
       
                }

                var service = new HTTPService<Response<Model.BaseModel>>(_configuration["Uri:WebAPI"]);
                service.Authentication = Request.Cookies["token"];
                service.Path = "evt";
                return service.PostAsync(body).Result;
            }
            catch (Exception e)
            {
                try
                {
                    if (ConvertTo.String(e.InnerException.Message).Equals("401"))
                    {
                        response.result = DataFactory.Fail;
                        response.statusCode = 401;
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
        public Response<CalendarEventInDayResponseModel> GetEventInDay([FromBody] EventRequestModel body)
        {
            var response = new Model.Response<CalendarEventInDayResponseModel>();
            if (JwtManager.IsEmptyOrInvalid(ConvertTo.String(Request.Cookies["token"])))
            {
                response.result = DataFactory.Fail;
                response.statusCode = 401;
                response.message = "กรุณาเข้าสู่ระบบ";
                return response;
            }
            try
            {
                var service = new HTTPService<Response<CalendarEventInDayResponseModel>>(_configuration["Uri:WebAPI"]);
                service.Authentication = Request.Cookies["token"];
                service.Path = "evt/in-day";
                return service.PostAsync(body).Result;
            }
            catch (Exception e)
            {
                try
                {
                    if (ConvertTo.String(e.InnerException.Message).Equals("401"))
                    {
                        response.result = DataFactory.Fail;
                        response.statusCode = 401;
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

        private string ValidateData(EventModel body)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(body.event_date_from))
            {
                sb.Append("- กรุณาระบุวันที่เริ่ม <br>");
            }
            else
            {
                var dateStart = ConvertTo.CustomDateTime(body.event_date_from);
                var dateEnd = ConvertTo.CustomDateTime(body.event_date_to);
                if (dateStart > dateEnd)
                {
                    sb.Append("- กรุณาระบุวันสิ้นสุด Event ให้มากกว่าวันเริ่ม Event <br>");
                }
            }
            if (string.IsNullOrWhiteSpace(body.event_name_th))
            {
                sb.Append("- กรุณาระบุชื่อ Event (ภาษาไทย) <br>");
            }
            if (string.IsNullOrWhiteSpace(body.event_name_en))
            {
                sb.Append("- กรุณาระบุชื่อ Event (ภาษาอังกฤษ) <br>");
            }
            if (string.IsNullOrWhiteSpace(body.register_total))
            {
                sb.Append("- กรุณาระบุจำนวนที่นั่ง <br>");
            }
            return sb.ToString();
        }

 

        [HttpPost]
        public async Task<ResponseFile> UploadFile(List<IFormFile> files, string language)
        {
            ResponseFile response = new ResponseFile();
            string uploadPath = $@"\file\event\{_configuration["EndPointEvent:Banner"]}";
            string fileNames = "";
            using (var ms = new MemoryStream())
            {
                foreach (var file in files)
                {
 
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    var bytes = Convert.FromBase64String(Convert.ToBase64String(fileBytes));
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{uploadPath}");
                    fileNames = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);

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
