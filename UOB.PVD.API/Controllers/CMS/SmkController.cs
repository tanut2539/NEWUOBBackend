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
using UOB.PVD.Repository.Smk;

namespace UOB.PVD.API.Controllers.CMS
{
    public class SmkController : RootController
    {
        private readonly IConfiguration _configuration;
        string connectionString = "";
        public SmkController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _configuration = configuration;
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

        [HttpPost]
        [Authorize]
        [Route("smk/create")]
        public Response<BaseModel> Create(SmkFileDTO body)
        {
            var response = new Response<BaseModel>();
            try
            {

                DataRow data = smk_file.SetData(connectionString, body);
                data["create_by"] = User.Identity.Name;
                data["create_date"] = DateTime.Now;
                data["active"] = 1;
                smk_file.Add(connectionString, data);
 
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
        [Route("smk/update")]
        public Response<BaseModel> Update(SmkFileDTO body)
        {
            var response = new Response<BaseModel>();
            try
            {

                DataRow data = smk_file.SetData(connectionString, body);
                data["modify_by"] = User.Identity.Name;
                data["modify_date"] = DateTime.Now;
                smk_file.Edit(connectionString, data);

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
        [Route("smk/delete")]
        public Response<BaseModel> Delete(SmkFileDTO body)
        {
            var response = new Response<BaseModel>();
            try
            {

                DataTable data = smk_file.GetList(connectionString, body.smk_file_seq);
                if (data is not null && data.Rows.Count > 0)
                {
                    smk_file.Delete(connectionString, body.smk_file_seq);
                    response.result = ConstantData.Success;
                    response.message = ConstantData.Success;
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

        [HttpPost]
        [Authorize]
        [Route("smk/getlist")]
        public Response<List<SmkFileDTO>> GetList(SmkFileDTO body)
        {
            var response = new Response<List<SmkFileDTO>>();
            try
            {
                var basePathFile = $"{_configuration["Uri:CMS"]}/{_configuration["EndPoint:Smk"]}";
                var data = smk_file.GetList(connectionString, basePathFile, body.smk_type);
                if (data is not null)
                {
                    response.Data = HelperFunctions.ConvertToList<SmkFileDTO>(data);
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
        [Route("smk/detail")]
        public Response<SmkFileDTO> GetDetail(SmkFileDTO body)
        {
            var response = new Response<SmkFileDTO>();
            try
            {
                var basePathFile = $"{_configuration["Uri:CMS"]}/{_configuration["EndPoint:Smk"]}";
                var data = smk_file.GetDetail(connectionString, basePathFile, body.smk_file_seq);
                if (data is not null)
                {
                    response.Data = HelperFunctions.ToObject<SmkFileDTO>(data.Rows[0]);
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
