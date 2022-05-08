using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UOB.PVD.Helper;
using UOB.PVD.Model;
using UOB.PVD.Model.FrontEnd;
using UOB.PVD.Repository.ChartFile;
using UOB.PVD.Repository.Master;
using UOB.PVD.Repository.policy;
using UOB.PVD.Repository.Smk;

namespace UOB.PVD.API.Controllers.FRONT
{
    [AllowAnonymous]
 
    public class HomeController : RootController
    {
        private readonly IConfiguration _configuration;
        string connectionString = "";
        public HomeController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _configuration = configuration;
        }
        string[] investment_type = { "IN", "OUT" };
        string ThaiLang = "th-TH";
        string DefaultLang = "th-TH";
        private mas_fund _mas_fund;
        private mas_fund mas_fund
        {
            set
            {
                _mas_fund = value;
            }
            get
            {
                if (_mas_fund == null)
                {
                    _mas_fund = new mas_fund();
                }
                return _mas_fund;
            }
        }

        private policy _policy;
        private policy policy
        {
            set
            {
                _policy = value;
            }
            get
            {
                if (_policy == null)
                {
                    _policy = new policy();
                }
                return _policy;
            }
        }

        private mas_policy_type _mas_policy_type;
        private mas_policy_type mas_policy_type
        {
            set
            {
                _mas_policy_type = value;
            }
            get
            {
                if (_mas_policy_type == null)
                {
                    _mas_policy_type = new mas_policy_type();
                }
                return _mas_policy_type;
            }
        }

        private smk_file _smk_file;
        private smk_file smk_file
        {
            set
            {
                _smk_file = value;
            }
            get
            {
                if (_smk_file == null)
                {
                    _smk_file = new smk_file();
                }
                return _smk_file;
            }
        }

        private chart_file _chart_file;
        private chart_file chart_file
        {
            set
            {
                _chart_file = value;
            }
            get
            {
                if (_chart_file == null)
                {
                    _chart_file = new chart_file();
                }
                return _chart_file;
            }
        }


        [HttpGet]
        [Route("front-end/index")]
        public Response<HomeDTO> Get()
        {
            string language = DefaultLang;
            Request.Headers.TryGetValue("lang", out var culture);
            language = ConvertTo.String(culture) != "" ? culture : language;

            var response = new Response<HomeDTO>();
            var responseData = new HomeDTO();
            //Tab 1
            // 1. กองทุนสำรองเลี้ยงชีพยูโอบี อินเวสเตอร์ ชอยส์ ซึ่งจดทะเบียนแล้ว (UOB Investor Choice)
            var DtFund = mas_fund.GetData(connectionString,"1");
            if (DtFund is not null && DtFund.Rows.Count > 0)
            {

                var FrontEndFundDTO = new FrontEndFundDTO();

                var Fund = HelperFunctions.ToObject<FundDTO>(DtFund.Rows[0]);
                FrontEndFundDTO.fund_seq = Fund.fund_seq;
                FrontEndFundDTO.fund_name = language == ThaiLang ? Fund.fund_name_th: Fund.fund_name_en;
               
                foreach (var _investment_type in investment_type)
                {
                    //ดึงการลงทุน
                    var InvestmentType = new FrontEndInvestmentType();
                    InvestmentType.investment_type_id = _investment_type;
                    FrontEndFundDTO.InvestmentType.Add(InvestmentType);

                    var DtPolicyType = mas_policy_type.FrontEndGetList(connectionString, "1", _investment_type);
                    var PolicyType = HelperFunctions.ConvertToList<PolicyTypeDTO>(DtPolicyType);
                    foreach (var _policyType in PolicyType)
                    {
                        //ดึงนโยบายของแต่ละการลงทุน
                        var _policyTypeDTO = new FrontEndPolicyType();
                        _policyTypeDTO.policy_type_id = _policyType.policy_type_id;
                        _policyTypeDTO.policy_type_name = language == ThaiLang ? _policyType.policy_type_name_th : _policyType.policy_type_name_en;
                        var basePathFiles = $"{_configuration["Uri:CMS"]}/{_configuration["EndPoint:Investor1In"]}";
                        var DtPolicy = policy.FrontEndGetList(connectionString,"1" , _investment_type, _policyType.policy_type_id, basePathFiles);
                        var Policy = HelperFunctions.ConvertToList<InvestorChoiceDTO>(DtPolicy);
                        foreach (var _policy in Policy)
                        {
                            var _frontEndPolicy = new FrontEndPolicy();
                            _frontEndPolicy.policy_seq = _policy.policy_seq;
                            _frontEndPolicy.policy_id = _policy.policy_id;
                            _frontEndPolicy.policy_title = language == ThaiLang ? _policy.policy_title_th : _policy.policy_title_en;
                            _frontEndPolicy.policy_detail = language == ThaiLang ? _policy.policy_detail_th : _policy.policy_detail_en;
                            _frontEndPolicy.show_with = _policy.show_with;
                            if (_policy.show_with is "PDF" )
                            {
                                _frontEndPolicy.url = language == ThaiLang ? _policy.policy_pdf_url_th : _policy.policy_pdf_url_en;
                            }
                            else
                            {
                                _frontEndPolicy.url = language == ThaiLang ? _policy.policy_goto_url_th : _policy.policy_goto_url_en;   
                            }
                            _policyTypeDTO.Policy.Add(_frontEndPolicy);
                        }
                        InvestmentType.PolicyType.Add(_policyTypeDTO);
                    }
                }
                responseData.UOBInvestorChoice = FrontEndFundDTO;

                var basePathFile = $"{_configuration["Uri:CMS"]}/{_configuration["EndPoint:Chart"]}";
                var ChartFile = chart_file.GetList(connectionString, basePathFile, "");
                if (ChartFile is not null && ChartFile.Rows.Count > 0)
                {
                    var _ChartFile = HelperFunctions.ConvertToList<ChartFileDTO>(ChartFile);
                    foreach (var item in _ChartFile)
                    {
                        var _FrontEndChartFile = new FrontEndChartFile();
                        _FrontEndChartFile.chart_file_seq = item.chart_file_seq;
                        _FrontEndChartFile.chart_title = language == ThaiLang ? item.chart_title_th : item.chart_title_en;
                        _FrontEndChartFile.chart_file_url = item.show_with == "URL" ? (language == ThaiLang ? item.chart_goto_url_th : item.chart_goto_url_en ) :
                                                              (language == ThaiLang ? item.chart_file_name_th_url : item.chart_file_name_en_url);
                        responseData.UOBInvestorChoice.ChartFile.Add(_FrontEndChartFile);
                    }
                }
            }

            //Tab 2
            //2. กองทุนสำรองเลี้ยงชีพ ยูโอบี มาสเตอร์ ฟันด์ ซึ่งจดทะเบียนแล้ว (UOB Master Fund)
            DtFund = mas_fund.GetData(connectionString, "2");
            if (DtFund is not null && DtFund.Rows.Count > 0)
            {
                var FrontEndFundDTO = new FrontEndFundDTO();

                var Fund = HelperFunctions.ToObject<FundDTO>(DtFund.Rows[0]);
                FrontEndFundDTO.fund_seq = Fund.fund_seq;
                FrontEndFundDTO.fund_name = language == ThaiLang ? Fund.fund_name_th : Fund.fund_name_en;

                foreach (var _investment_type in investment_type)
                {
                    //ดึงการลงทุน
                    var InvestmentType = new FrontEndInvestmentType();
                    InvestmentType.investment_type_id = _investment_type;
                    FrontEndFundDTO.InvestmentType.Add(InvestmentType);

                    var DtPolicyType = mas_policy_type.FrontEndGetList(connectionString, "2", _investment_type);
                    var PolicyType = HelperFunctions.ConvertToList<PolicyTypeDTO>(DtPolicyType);
                    foreach (var _policyType in PolicyType)
                    {
                        //ดึงนโยบายของแต่ละการลงทุน
                        var _policyTypeDTO = new FrontEndPolicyType();
                        _policyTypeDTO.policy_type_id = _policyType.policy_type_id;
                        _policyTypeDTO.policy_type_name = language == ThaiLang ? _policyType.policy_type_name_th : _policyType.policy_type_name_en;
                        var basePathFile = $"{_configuration["Uri:CMS"]}/{_configuration["EndPoint:Investor1In"]}";
                        var DtPolicy = policy.FrontEndGetList(connectionString, "2", _investment_type, _policyType.policy_type_id, basePathFile);
                        var Policy = HelperFunctions.ConvertToList<InvestorChoiceDTO>(DtPolicy);
                        foreach (var _policy in Policy)
                        {
                            var _frontEndPolicy = new FrontEndPolicy();
                            _frontEndPolicy.policy_seq = _policy.policy_seq;
                            _frontEndPolicy.policy_id = _policy.policy_id;
                            _frontEndPolicy.policy_title = language == ThaiLang ? _policy.policy_title_th : _policy.policy_title_en;
                            _frontEndPolicy.policy_detail = language == ThaiLang ? _policy.policy_detail_th : _policy.policy_detail_en;
                            _frontEndPolicy.show_with = _policy.show_with;
                            if (_policy.show_with is "PDF")
                            {
                                _frontEndPolicy.url = language == ThaiLang ? _policy.policy_pdf_url_th : _policy.policy_pdf_url_en;
                            }
                            else
                            {
                                _frontEndPolicy.url = language == ThaiLang ? _policy.policy_goto_url_th : _policy.policy_goto_url_en;
                            }
                            _policyTypeDTO.Policy.Add(_frontEndPolicy);
                        }
                        InvestmentType.PolicyType.Add(_policyTypeDTO);
                    }
                }
                responseData.UOBMasterFund = FrontEndFundDTO;
            }

            response.Data = responseData;
            return response;
        }


        [HttpGet]
        [Route("front-end/sheet")]
        public Response<FrontEndSheet> GetSheet()
        {
            string language = DefaultLang;
            Request.Headers.TryGetValue("lang", out var culture);

            language = ConvertTo.String(culture) != "" ? culture : language;

            var response = new Response<FrontEndSheet>();
 
            var basePathFile = $"{_configuration["Uri:CMS"]}/{_configuration["EndPoint:Investor1In"]}";
            var DtPolicy = policy.FrontEndGetList(connectionString, "2",  basePathFile);  
            var Policy = HelperFunctions.ConvertToList<InvestorChoiceDTO>(DtPolicy);
            foreach (var _policy in Policy)
            {
                var _frontEndPolicy = new FrontEndPolicy();
                _frontEndPolicy.policy_seq = _policy.policy_seq;
                _frontEndPolicy.policy_id = _policy.policy_id;
                _frontEndPolicy.policy_title = language == ThaiLang ? _policy.policy_title_th : _policy.policy_title_en;
                _frontEndPolicy.policy_detail = language == ThaiLang ? _policy.policy_detail_th : _policy.policy_detail_en;
                _frontEndPolicy.show_with = _policy.show_with;
                if (_policy.show_with is "PDF")
                {
                    _frontEndPolicy.url = language == ThaiLang ? _policy.policy_pdf_url_th : _policy.policy_pdf_url_en;
                }
                else
                {
                    _frontEndPolicy.url = language == ThaiLang ? _policy.policy_goto_url_th : _policy.policy_goto_url_en;
                }
                response.Data.UOBMasterFund.Add(_frontEndPolicy);
            }

            DtPolicy = policy.FrontEndGetList(connectionString, "1", basePathFile); //tab1
            Policy = HelperFunctions.ConvertToList<InvestorChoiceDTO>(DtPolicy);
            foreach (var _policy in Policy)
            {
                var _frontEndPolicy = new FrontEndPolicy();
                _frontEndPolicy.policy_seq = _policy.policy_seq;
                _frontEndPolicy.policy_id = _policy.policy_id;
                _frontEndPolicy.policy_title = language == ThaiLang ? _policy.policy_title_th : _policy.policy_title_en;
                _frontEndPolicy.policy_detail = language == ThaiLang ? _policy.policy_detail_th : _policy.policy_detail_en;
                _frontEndPolicy.show_with = _policy.show_with;
                if (_policy.show_with is "PDF")
                {
                    _frontEndPolicy.url = language == ThaiLang ? _policy.policy_pdf_url_th : _policy.policy_pdf_url_en;
                }
                else
                {
                    _frontEndPolicy.url = language == ThaiLang ? _policy.policy_goto_url_th : _policy.policy_goto_url_en;
                }
                response.Data.UOBInvestorChoice.Add(_frontEndPolicy);
 
            }
            var basePathSmkFile = $"{_configuration["Uri:CMS"]}/{_configuration["EndPoint:Smk"]}";
            var DtSmk = smk_file.GetList(connectionString , basePathSmkFile, "SMK1");
            if (DtSmk is not null && DtSmk.Rows.Count > 0)
            {
                var FrontEndSmk = HelperFunctions.ConvertToList<SmkFileDTO>(DtSmk);
                foreach (var _Smk in FrontEndSmk)
                {
                    var _FrontEndSmk = new FrontEndSmk();
                    _FrontEndSmk.smk_file_seq = _Smk.smk_file_seq;
                    _FrontEndSmk.smk_file_title = language == ThaiLang ? _Smk.smk_title_th : _Smk.smk_title_en;
                    _FrontEndSmk.smk_file_url = language == ThaiLang ? _Smk.smk_file_name_th_url : _Smk.smk_file_name_en_url;
                    _FrontEndSmk.smk_type = _Smk.smk_type;

                    response.Data.Smk1.Add(_FrontEndSmk);
                }
            }

 
            DtSmk = smk_file.GetList(connectionString, basePathSmkFile, "SMK2");
            if (DtSmk is not null && DtSmk.Rows.Count > 0)
            {
                var FrontEndSmk = HelperFunctions.ConvertToList<SmkFileDTO>(DtSmk);
                foreach (var _Smk in FrontEndSmk)
                {
                    var _FrontEndSmk = new FrontEndSmk();
                    _FrontEndSmk.smk_file_seq = _Smk.smk_file_seq;
                    _FrontEndSmk.smk_file_title = language == ThaiLang ? _Smk.smk_title_th : _Smk.smk_title_en;
                    _FrontEndSmk.smk_file_url = language == ThaiLang ? _Smk.smk_file_name_th_url : _Smk.smk_file_name_en_url;
                    _FrontEndSmk.smk_type = _Smk.smk_type;
                    response.Data.Smk2.Add(_FrontEndSmk);
                }
            }
            basePathFile = $"{_configuration["Uri:CMS"]}/{_configuration["EndPoint:Chart"]}";
            var ChartFile = chart_file.GetList(connectionString, basePathFile,"");
            if (ChartFile is not null && ChartFile.Rows.Count > 0)
            {
                var FrontEndSmk = HelperFunctions.ConvertToList<ChartFileDTO>(ChartFile);
                foreach (var item in FrontEndSmk)
                {
                    var _FrontEndChartFile = new FrontEndChartFile();
                    _FrontEndChartFile.chart_file_seq = item.chart_file_seq;
                    _FrontEndChartFile.chart_title = language == ThaiLang ? item.chart_title_th : item.chart_title_en;
                    _FrontEndChartFile.chart_file_url = item.show_with == "URL" ? (language == ThaiLang ? item.chart_goto_url_th  : item.chart_goto_url_en ) :
                                                  (language == ThaiLang ? item.chart_file_name_th_url : item.chart_file_name_en_url);
                    response.Data.ChartFile.Add(_FrontEndChartFile);
                }
            }

            return response;
        }

    }
}
