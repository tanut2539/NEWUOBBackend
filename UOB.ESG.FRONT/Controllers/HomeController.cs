using Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UOB.ESG.FRONT.Models;
using UOB.PVD.Helper;
using UOB.PVD.Model.ESG;
using UOB.PVD.Model.ESG.FrontEnd;

namespace UOB.ESG.FRONT.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration m_config;
        private AppSetting _appSettings;
        public HomeController(IConfiguration config, IOptions<AppSetting> setting)
        {
            m_config = config;
            _appSettings = setting.Value;
        }

        public IActionResult index(string size)
        {
            var Culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture;
            Response<FrontEndHomeModel> ViewModel = new Response<FrontEndHomeModel>();

            try
            {
                //Send To API
                var service = new HTTPService<Response<FrontEndHomeModel>>(m_config["Uri:WebAPI"]);
                service.Path = "/index/load-main-data";
                service.ContentLanguage = ConvertTo.String(Culture);
                ViewModel = service.PostAsync().Result;

                if (ViewModel.result.Equals(ConstantData.Success))
                {
                    ViewBag.language = ConvertTo.String(Culture);
                    return View(ViewModel.Data);
                }
                return RedirectToAction(nameof(warn), new { message = ViewModel.message });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(warn), new { message = e.Message });
            }




            #region Comment Old Vervion
            //int page_size = ConvertTo.Int(size);
            //page_size += 3;
            //DataTable dt_banner = null;
            //DataTable Article = null;
            //DataTable Front = null;

            //if(ConvertTo.String(Culture) == "th-TH")
            //{
            //    dt_banner = banner.FrontEndGetListTH(connectionString, _appSettings.CMS_URL + "FileUpload/Banner/");
            //    Article = article.FrontEndGetListTH(connectionString, _appSettings.CMS_URL + "FileUpload/Article/", page_size);
            //    Front = fund.FrontEndGetListTH(connectionString, _appSettings.CMS_URL + "FileUpload/Fund/");
            //}
            //else
            //{
            //    dt_banner = banner.FrontEndGetListEN(connectionString, _appSettings.CMS_URL + "FileUpload/Banner/");
            //    Article = article.FrontEndGetListEN(connectionString, _appSettings.CMS_URL + "FileUpload/Article/", page_size);
            //    Front = fund.FrontEndGetListEN(connectionString, _appSettings.CMS_URL + "FileUpload/Fund/");
            //}

            //List<FrontEndBannerModel> _banner = HelperFunctions.ConvertToList<FrontEndBannerModel>(dt_banner);
            //ViewModel.Banner = _banner;

            //List<FrontEndArticleModel> _article = HelperFunctions.ConvertToList<FrontEndArticleModel>(Article);
            //ViewModel.Article = _article;

            //List<FrontEndFundModel> _front = HelperFunctions.ConvertToList<FrontEndFundModel>(Front);
            //ViewModel.Fund = _front;
            #endregion

        }
        [HttpGet]
        [Route("etf")]
        public IActionResult rtf(string lang)
        {
            var Culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture;
            ViewBag.language = ConvertTo.String(Culture);
            return View();
        }
        [HttpGet]
        [Route("appoach")]
        public IActionResult Appoach(string id)
        {
            var Culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture;
            ViewBag.language = ConvertTo.String(Culture);
            return View();
        }
        [HttpGet]
        [Route("article/{id}")]
        public IActionResult Article(string id)
        {
            var Culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture;
            var ViewModel = new Response<FrontEndArticleDetailModel>();
            try
            {
                var service = new HTTPService<Response<FrontEndArticleDetailModel>>(m_config["Uri:WebAPI"]);
                service.Path = "article";
                service.ContentLanguage = ConvertTo.String(Culture);
                ViewModel = service.GetAsync(id).Result;
                if (ViewModel.result.Equals(ConstantData.Success))
                {
                    ViewModel.Data.str_publish_date = ConvertTo.StringThaiDate(ViewModel.Data.publish_date);
                    ViewModel.Data.thisURL = string.Format("{0}article/{1}", _appSettings.FRONT_URL, id);
                    ViewBag.language = ConvertTo.String(Culture);
                    ViewBag.current_id = ConvertTo.String(id);
                    ViewData["Title"] = ViewModel.Data.title;
                    return View(ViewModel.Data);
                }
                return RedirectToAction(nameof(warn), new { message = ViewModel.message });

            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

        }


        public IActionResult warn(string message)
        {
            ViewBag.message = message;
            return View();
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
        public IActionResult Changelanguage(string controllerName, string actionName, string id)
        {

            var Culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture;
            var _lang = "en-US";
            if (Convert.ToString(Culture) == "en-US")
            {
                _lang = "th-TH";
            }
            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(_lang));
            var option = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) };
            Response.Cookies.Append("Web.Language", cookieValue, option);
            if (ConvertTo.String(controllerName).ToUpper().Equals("HOME"))
            {
                switch (ConvertTo.String(actionName).ToUpper())
                {
                    case "INDEX":
                        return RedirectToAction(nameof(Index));
                    case "APPOACH":
                        return RedirectToAction(nameof(Appoach));
                    case "ARTICLE":
                        return RedirectToAction(nameof(Article), new { id = id });
                    case "RTF":
                        return RedirectToAction(nameof(rtf));

                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("redirectToArticle/{id}/{lang}")]
        public IActionResult RedirectToArticle(string id, string lang)
        {
            if (string.IsNullOrWhiteSpace(lang))
            {
                return RedirectToAction(nameof(Article), new { id = id });

            }
            var _lang = "en-US";
            if (lang == "th-TH")
            {
                _lang = "th-TH";
            }
            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(_lang));
            var option = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) };
            Response.Cookies.Append("Web.Language", cookieValue, option);
            return RedirectToAction(nameof(Article), new { id = id });

        }
        [HttpGet]
        [Route("redirectToAppoach/{lang}")]
        public IActionResult RedirectToAppoach(string lang)
        {
            if (string.IsNullOrWhiteSpace(lang))
            {
                return RedirectToAction(nameof(Appoach));
            }
            var _lang = "en-US";
            if (lang == "th-TH")
            {
                _lang = "th-TH";
            }
            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(_lang));
            var option = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) };
            Response.Cookies.Append("Web.Language", cookieValue, option);
            return RedirectToAction(nameof(Appoach));
        }
        [HttpPost]
        public Response<ResponseFrontEndLoadMoreArticleModel> LoadArticleMore([FromBody] FrontEndLoadMoreArticleModel body)
        {

            var Culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture;
            ViewBag.language = ConvertTo.String(Culture);
            Response<ResponseFrontEndLoadMoreArticleModel> ViewModel = new Response<ResponseFrontEndLoadMoreArticleModel>();

            try
            {
                //Send To API
                var service = new HTTPService<Response<ResponseFrontEndLoadMoreArticleModel>>(m_config["Uri:WebAPI"]);
                service.Path = "/article/getlist";
                service.ContentLanguage = ConvertTo.String(Culture);
                return service.PostAsync(body).Result;

            }
            catch (Exception e)
            {
                ViewModel.result = ConstantData.Fail;
                ViewModel.message = e.Message;
                return ViewModel;
            }


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
