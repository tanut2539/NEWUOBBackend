using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Helper
{
    public class ConvertTo
    {

        public static string String(object val)
        {
            return Convert.ToString(val);
        }
        public static int Int(object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return 0;
            }
        }
        public static object StringForDatabase(object val)
        {
            if (Convert.ToString(val) != "undefined")
            {
                if (string.IsNullOrEmpty(Convert.ToString(val)))
                    return DBNull.Value;
                else
                    return val;
            }
            else
            {
                return DBNull.Value;
            }

        }
        public static object IntForDatabase(object val)
        {
            try
            {
                if (val != null)
                    return Convert.ToInt32(val);
                else
                    return DBNull.Value;

            }
            catch
            {
                return DBNull.Value;
            }

        }

        public static object DecimalForDatabase(object val)
        {
            try
            {
                if (val != null)
                    return Convert.ToDecimal(val);
                else
                    return DBNull.Value;

            }
            catch
            {
                return DBNull.Value;
            }

        }

        public static string StringThaiDate(DateTime val)
        {
            try
            {
                string[] th_month = { "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค." };
                int _day = val.Day;
                int _month = val.Month;
                int _year = val.Year < 2300 ? val.Year + 543 : val.Year;
                return string.Format("{0} {1} {2}", _day, th_month[_month - 1], _year);
            }
            catch
            {
                return "";
            }

        }
        public static string generateID(string url_add)
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            string number = string.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);

            return number;
        }
        public static DateTime CustomDateTime(string date)
        {
            try
            {
                if (date.ToUpper().Contains('T'))
                {
                    string[] sDateTime = date.Split('T');
                    string[] sDate = sDateTime[0].Split('-');
                    string[] sTime = sDateTime[1].Split(':');
                    DateTime d = new DateTime(ConvertTo.Int(sDate[0]),
                         ConvertTo.Int(sDate[1]),
                         ConvertTo.Int(sDate[2]),
                         ConvertTo.Int(sTime[0]),
                         ConvertTo.Int(sTime[1]), 0);
                    return d;
                }
                else
                {
                    string[] sDateTime = date.Split(' ');
                    string[] sDate = sDateTime[0].Split('/');
                    string[] sTime = sDateTime[1].Split(':');
                    DateTime d = new DateTime(ConvertTo.Int(sDate[2]),
                        ConvertTo.Int(sDate[1]),
                        ConvertTo.Int(sDate[0]),
                        ConvertTo.Int(sTime[0]),
                        ConvertTo.Int(sTime[1]), 0);
                    return d;
                }
            }
            catch 
            {
                return DateTime.MinValue;
            }
        }
        public static DateTime CustomDateTimeWithDDMMYYYY(string date)
        {
            try
            {
            
                string[] sDate = date.Split('/');
 
                return new DateTime(ConvertTo.Int(sDate[2]),
                    ConvertTo.Int(sDate[1]),
                    ConvertTo.Int(sDate[0]));
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
    }
}
