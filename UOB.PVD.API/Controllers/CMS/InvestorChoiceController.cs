using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UOB.PVD.Helper;
using UOB.PVD.Model;
using UOB.PVD.Repository.policy;

namespace UOB.PVD.API.Controllers.CMS
{
    public class InvestorChoiceController : RootController
    {
        private readonly IConfiguration _configuration;
        string connectionString = "";
        public InvestorChoiceController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _configuration = configuration;
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

        [HttpGet]
        [Authorize]
        [Route("investor-choice/{id}/{policy_type_id}")]
        public Response<InvestorChoiceDTO> Get(string id ,string policy_type_id)
        {
            var response = new Response<InvestorChoiceDTO>();
            try
            {
                if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(policy_type_id))
                {
                    var basePathFile = $"{_configuration["Uri:CMS"]}/{_configuration["EndPoint:Investor1In"]}";
                    DataRow data = policy.GetData(connectionString, id, policy_type_id, basePathFile);
                    if (data is not null)
                    {
                        response.Data = HelperFunctions.ToObject<InvestorChoiceDTO>(data);
                    }
                }
                else
                {
                    response.result = ConstantData.Fail;
                    response.message = "ข้อมูลไม่ครบถ้วน";
                } 
                return response;
            }
            catch (Exception e)
            {
                response.result = ConstantData.Fail;
                response.message = e.ToString();
            }
            return response;
        }

        [HttpPost]
        [Authorize]
        [Route("investor-choice/list")]
        public Response<List<InvestorChoiceListDTO>> GetList(InvestorChoiceSearchListDTO dto)
        {
            var response = new Response<List<InvestorChoiceListDTO>>();
            try
            {
                

                DataTable data = policy.GetListForInvestorChoice(connectionString, dto);
                if (data is not null)
                {
                    response.Data = HelperFunctions.ConvertToList<InvestorChoiceListDTO>(data);
                }


                return response;


            }
            catch (Exception e)
            {
                response.result = ConstantData.Fail;
                response.message = e.ToString();
            }

            return response;

        }

        [HttpPost]
        [Authorize]
        [Route("investor-choice/create")]
        public Response<BaseModel> Create(InvestorChoiceDTO body)
        {
            var response = new Response<BaseModel>();
            try
            {
        
                DataRow data = policy.SetData(connectionString, body);
                data["policy_id"] = ConvertTo.StringForDatabase(Guid.NewGuid());
                data["create_by"] = User.Identity.Name;
                data["create_date"] = DateTime.Now;
                data["active"] = 1;
                var Dtpolicy = policy.Add(connectionString, data);
                if (Dtpolicy is not null && Dtpolicy.Rows.Count > 0)
                {
                    response.Data.current_id = $"{ConvertTo.String(Dtpolicy.Rows[0]["policy_seq"])}&policy_type={ConvertTo.String(Dtpolicy.Rows[0]["policy_type_id"])}";
                }
            }
            catch (Exception e)
            {
                response.result = ConstantData.Fail;
                response.message = e.ToString();
            }

            return response;
 
        }

        [HttpPost]
        [Authorize]
        [Route("investor-choice/update")]
        public Response<BaseModel> Update(InvestorChoiceDTO body)
        {
            var response = new Response<BaseModel>();
            response.Data.current_id = $"{body.policy_seq}&policy_type={body.policy_type_id}";
            try
            {
                DataRow data = policy.SetData(connectionString, body);
                data["modify_by"] = User.Identity.Name;
                data["modify_date"] = DateTime.Now;
                policy.Edit(connectionString, data);
            }
            catch (Exception e)
            {
                response.result = ConstantData.Fail;
                response.message = e.ToString();
            }

            return response;



        }

        [HttpPost]
        [Authorize]
        [Route("investor-choice/delete")]
        public Response<BaseModel> Delete(InvestorChoiceDTO body)
        {
            var response = new Response<BaseModel>();
            response.Data.current_id = body.policy_seq;
            try
            {
                if (!string.IsNullOrWhiteSpace(body.policy_seq) && !string.IsNullOrWhiteSpace(body.policy_type_id) )
                {
                    var basePathFile = $"{_configuration["Uri:CMS"]}/{_configuration["EndPoint:Investor1In"]}";
                    DataRow data = policy.GetData(connectionString, body.policy_seq, body.policy_type_id, basePathFile);
                    if (data is not null)
                    {
                        policy.Delete(connectionString, body.policy_seq);
                    }
                    else
                    {
                        response.result = ConstantData.Fail;
                        response.message = "ไม่พบข้อมูล";
                    }
                }
                else
                {
                    response.result = ConstantData.Fail;
                    response.message = "ไม่พบข้อมูล";
                }
            }
            catch (Exception e)
            {
                response.result = ConstantData.Fail;
                response.message = e.ToString();
            }
            return response;
        }

    }
}
