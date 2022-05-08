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
using UOB.PVD.Model.EVT;
using UOB.PVD.Repository.EVT;

namespace UOB.PVD.API.Controllers.EVT.CMS
{
 
    public class EvtController : RootController
    {
        private readonly IConfiguration _configuration;
        string connectionString = "";
        public EvtController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("EvtConnection");
            _configuration = configuration;
        }

        private evevt _evevt;
        private evevt evevt
        {
            set
            {
                _evevt = value;
            }
            get
            {
                if (_evevt == null)
                {
                    _evevt = new evevt();
                }
                return _evevt;
            }
        }

        

        [HttpPost]
        [Authorize]
        [Route("evt")]
        public Response<BaseModel> Create(EventModel body)
        {
            var response = new Response<BaseModel>();
            try
            {
                body.event_id = Guid.NewGuid().ToString();
                DataRow data = evevt.SetData(connectionString, body);
                data["create_by"] = User.Identity.Name;
                data["create_date"] = DateTime.Now;
                data["active"] = 0;
                evevt.Add(connectionString, data);

                response.Data.current_id = body.event_id;
                return response;
            }
            catch (Exception e)
            {
                response.result = ConstantData.Fail;
                response.message = e.ToString();
            }
            return response;
        }

        [HttpGet]
        [Authorize]
        [Route("evt/calendar")]
        public Response<CmsCalendarResponseModel> GetEventInCalendar()
        {
            var response = new Response<CmsCalendarResponseModel>();
            try
            {
 
                DataTable data = evevt.GetListEventInCalendar(connectionString);
                if (data is not null)
                {
                    response.Data.calendar = HelperFunctions.ConvertToList<EventInCalendarResponseModel>(data);
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
        [Route("evt/in-day")]
        public Response<CalendarEventInDayResponseModel> GetEventInDay(EventRequestModel body)
        {
            var response = new Response<CalendarEventInDayResponseModel>();
            try
            {
                DateTime date = ConvertTo.CustomDateTimeWithDDMMYYYY(body.thaiDate);
                var basePathFile = $"{_configuration["Uri:CMS"]}/{_configuration["EndPointEvent:Banner"]}";
                DataTable data = evevt.GetListEventInDay(connectionString, basePathFile, date);
                if (data is not null)
                {
                    response.Data.events = HelperFunctions.ConvertToList<EventInDayResponseModel>(data);
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
        [Route("evt/get-data")]
        public Response<CalendarEventInDayResponseModel> GetData(EventRequestModel body)
        {
            var response = new Response<CalendarEventInDayResponseModel>();
            try
            {
                DateTime date = ConvertTo.CustomDateTimeWithDDMMYYYY(body.thaiDate);
                var basePathFile = $"{_configuration["Uri:CMS"]}/{_configuration["EndPointEvent:Banner"]}";
                DataRow data = evevt.GetData(connectionString, basePathFile, body.event_id);             
                if (data is not null)
                {
                    var rr = HelperFunctions.ToObject<EventInDayResponseModel>(data);
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

    }
}
