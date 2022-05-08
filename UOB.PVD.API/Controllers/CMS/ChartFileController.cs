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
using UOB.PVD.Repository.ChartFile;

namespace UOB.PVD.API.Controllers.CMS
{
    public class ChartFileController : RootController
    {
        private readonly IConfiguration _configuration;
        string connectionString = "";
        public ChartFileController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _configuration = configuration;
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


        [HttpPost]
        [Authorize]
        [Route("chart-file/create")]
        public Response<BaseModel> Create(ChartFileDTO body)
        {
            var response = new Response<BaseModel>();
            try
            {

                DataRow data = chart_file.SetData(connectionString, body);
                data["create_by"] = User.Identity.Name;
                data["create_date"] = DateTime.Now;
                data["active"] = 1;
                data["can_delete"] = true;
                chart_file.Add(connectionString, data);

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
        [Route("chart-file/update")]
        public Response<BaseModel> Update(ChartFileDTO body)
        {
            var response = new Response<BaseModel>();
            try
            {

                DataRow data = chart_file.SetData(connectionString, body);
                data["modify_by"] = User.Identity.Name;
                data["modify_date"] = DateTime.Now;
                chart_file.Edit(connectionString, data);

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
        [Route("chart-file/delete")]
        public Response<BaseModel> Delete(ChartFileDTO body)
        {
            var response = new Response<BaseModel>();
            try
            {

                DataTable data = chart_file.GetList(connectionString, body.chart_file_seq);
                if (data is not null && data.Rows.Count > 0)
                {
                    var result = HelperFunctions.ToObject<ChartFileDTO>(data.Rows[0]);
                    if (!result.can_delete)
                    {
                        response.result = ConstantData.Fail;
                        response.message = "ไม่สามารถลบรายการนี้ได้ เนื่องจากระบบกำหนด";
                    }
                    else
                    {
                        chart_file.Delete(connectionString, body.chart_file_seq);
                        response.result = ConstantData.Success;
                        response.message = ConstantData.Success;
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

        [HttpPost]
        [Authorize]
        [Route("chart-file/getlist")]
        public Response<List<ChartFileDTO>> GetList(ChartFileDTO body)
        {
            var response = new Response<List<ChartFileDTO>>();
            try
            {
 
                var data = chart_file.GetList(connectionString, "");
                if (data is not null)
                {
                    response.Data = HelperFunctions.ConvertToList<ChartFileDTO>(data);
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
        [Route("chart-file/detail")]
        public Response<ChartFileDTO> GetDetail(ChartFileDTO body)
        {
            var response = new Response<ChartFileDTO>();
            try
            {
                var basePathFile = $"{_configuration["Uri:CMS"]}/{_configuration["EndPoint:Chart"]}";
                var data = chart_file.GetDetail(connectionString, basePathFile, body.chart_file_seq);
                if (data is not null)
                {
                    response.Data = HelperFunctions.ToObject<ChartFileDTO>(data.Rows[0]);
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
