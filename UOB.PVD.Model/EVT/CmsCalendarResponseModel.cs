using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Model.EVT
{
    public class CmsCalendarResponseModel
    {
        public List<EventInCalendarResponseModel> calendar { get; set; }
    }
    public class  CalendarEventInDayResponseModel
    {
        public List<EventInDayResponseModel> events { get; set; }
    }

    public class EventInDayResponseModel
    {
        public string event_id { get; set; }
        public string event_name_th { get; set; }
        public string event_name_en { get; set; }
        public string banner { get; set; }
        public string register { get; set; }
    }


    public class EventInCalendarResponseModel
    {
        public string start { get; set; }
        public string title { get; set; }
        public string color { get; set; }
        public string textColor { get; set; }
    }
}
