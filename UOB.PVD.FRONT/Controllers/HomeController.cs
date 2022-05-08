using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UOB.PVD.FRONT.Models;
using UOB.PVD.Helper;
using UOB.PVD.Model;
using UOB.PVD.Model.FrontEnd;

namespace UOB.PVD.FRONT.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
 
        public async Task<IActionResult> IndexAsync()
        {
            var Culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture;
            var response = new HomeDTO();

            try
            {
                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "front-end/index";
                service.ContentLanguage = ConvertTo.String(Culture);
                var result = service.GetAsync();
                if (result.StatusCode is HttpStatusCode.OK)
                {
                    string contents = await result.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<Response<HomeDTO>>(contents).Data;
                }
            }
            catch { }


            return View(response);
        }


        [HttpGet]
        [Route("sheet")]
        public async Task<IActionResult> SheetAsync()
        {
            var Culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture;
            var response = new Response<FrontEndSheet>();
            var ViewModel = new FrontEndSheet();
            try
            {
                var service = new HTTPService(_configuration["Uri:WebAPI"]);
                service.Path = "/front-end/sheet";
                service.ContentLanguage = ConvertTo.String(Culture);
                var result = service.GetAsync();
                if (result.StatusCode is HttpStatusCode.OK)
                {
                    string contents = await result.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<Response<FrontEndSheet>> (contents);
                    ViewModel = response.Data;
                }
            }
            catch { }


            return View(ViewModel);
        }

        [HttpGet]
        [Route("index/{lang}")]
        public IActionResult RedirectToIndex(string lang)
        {
 
            var _lang = "en-US";
            if (lang == "th-TH")
            {
                _lang = "th-TH";
            }
            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(_lang));
            var option = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) };
            Response.Cookies.Append("Web.Language", cookieValue, option);
            return RedirectToAction(nameof(Index));



        }

        [HttpGet]
        [Route("sheet/{lang}")]
        public IActionResult RedirectToSheet(string lang)
        {

            var _lang = "en-US";
            if (lang == "th-TH")
            {
                _lang = "th-TH";
            }
            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(_lang));
            var option = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) };
            Response.Cookies.Append("Web.Language", cookieValue, option);
            return RedirectToAction("Sheet", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
