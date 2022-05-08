using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UOB.PVD.Helper;
using UOB.PVD.Model.ESG;
using UOB.PVD.Model.ESG.FrontEnd;
using UOB.PVD.Repository.Article;
using UOB.PVD.Repository.Banner;
using UOB.PVD.Repository.Fund;

namespace UOB.PVD.API.Controllers.ESG.FRONT
{
    [ApiExplorerSettings(GroupName = "FrontEnd")]
    public class IndexController : RootController
    {
        string connectionString = "";
        private readonly IConfiguration m_config;
        private AppSetting _appSettings;
        public IndexController(IConfiguration config, IOptions<AppSetting> setting)
        {
            m_config = config;
            connectionString = m_config.GetConnectionString("ESGConnection");
            _appSettings = setting.Value;
        }
        private banner _banner;
        private banner banner
        {
            set
            {
                _banner = value;
            }
            get
            {
                if (_banner == null)
                {
                    _banner = new banner();
                }
                return _banner;
            }
        }
        private article _article;
        private article article
        {
            set
            {
                _article = value;
            }
            get
            {
                if (_article == null)
                {
                    _article = new article();
                }
                return _article;
            }
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

        [HttpPost]
        [Route("index/load-main-data")]
        public Response<FrontEndHomeModel> LoadMainData()
        {
            Response<FrontEndHomeModel> response = new Response<FrontEndHomeModel>();
            Request.Headers.TryGetValue("lang", out var culture);
            try
            {
                FrontEndHomeModel ViewModel = new FrontEndHomeModel();

                DataTable dt_banner = ConvertTo.String(culture).Equals("th-TH")
                    ? banner.FrontEndGetListTH(connectionString, m_config["Uri:CMS"] + "FileUpload/Banner/")
                    : banner.FrontEndGetListEN(connectionString, m_config["Uri:CMS"] + "FileUpload/Banner/");
                DataTable Article = ConvertTo.String(culture).Equals("th-TH")
                    ? article.FrontEndGetListTH(connectionString, m_config["Uri:CMS"] + "FileUpload/Article/", 3)
                    : article.FrontEndGetListEN(connectionString, m_config["Uri:CMS"] + "FileUpload/Article/", 3);
                DataTable Front = ConvertTo.String(culture).Equals("th-TH")
                    ? fund.FrontEndGetListTH(connectionString, m_config["Uri:CMS"] + "FileUpload/Fund/")
                    : fund.FrontEndGetListEN(connectionString, m_config["Uri:CMS"] + "FileUpload/Fund/");

                List<FrontEndBannerModel> _banner = HelperFunctions.ConvertToList<FrontEndBannerModel>(dt_banner);
                ViewModel.Banner = _banner;

                List<FrontEndArticleModel> _article = HelperFunctions.ConvertToList<FrontEndArticleModel>(Article);
                ViewModel.Article = _article;

                List<FrontEndFundModel> _front = HelperFunctions.ConvertToList<FrontEndFundModel>(Front);
                ViewModel.Fund = _front;

                response.Data = ViewModel;
            }
            catch (Exception e)
            {
                response.result = ConstantData.Fail;
                response.message = e.Message;
            }

            return response;
        }
    }
}
