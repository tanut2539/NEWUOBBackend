using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Model.EVT
{
    public class EventModel
    {
        public string event_id { get; set; }
        public string event_code { get; set; }
        public string event_name_th { get; set; }
        public string event_name_en { get; set; }
        public string event_date_from { get; set; }
        public string event_time_from { get; set; }
        public string event_date_to { get; set; }
        public string event_time_to { get; set; }
        public string event_type_th { get; set; }
        public string event_type_en { get; set; }
        public string event_at_th { get; set; }
        public string event_at_en { get; set; }
        public string register_total { get; set; }
        public string register_schedule_from { get; set; }
        public string register_schedule_to { get; set; }
        public string fee { get; set; }
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string modify_by { get; set; }
        public string modify_date { get; set; }
        public bool is_register_by_qrcode { get; set; }
        public string qr_code { get; set; }
        public string event_status { get; set; }
        public string active { get; set; }
        public string event_detail_th { get; set; }
        public string event_detail_en { get; set; }
        public string event_banner_th { get; set; }
        public string url_event_banner_th { get; set; }
        public string event_banner_en { get; set; }
        public string url_event_banner_en { get; set; }

    }


}
