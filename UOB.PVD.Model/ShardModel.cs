using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Model
{
    public class ShardModel
    {
    }

    public class ResponseFile : BaseResponse
    {
        public string FileOriginalName { get; set; }
        public string name { get; set; }
        public string url { get; set; }

    }
    public class BaseDatatableSearch
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string search_by { get; set; }
        public string search_text { get; set; }
    }

    public class BaseResponse
    {
        private string _result;
        public string result
        {
            set
            {
                _result = value;
            }
            get
            {
                if (_result == null)
                {
                    _result = "success";
                }
                return _result;
            }
        }
        private string _message;
        public string message
        {
            set
            {
                _message = value;
            }
            get
            {
                if (_message == null)
                {
                    _message = "success";
                }
                return _message;
            }
        }
        public int id { get; set; }
    }

    public class Response<T> where T : new()
    {
        protected T GetData()
        {
            return new T();
        }


        public int statusCode { get; set; }


        private string _result;
        public string result
        {
            set
            {
                _result = value;
            }
            get
            {
                if (_result == null)
                {
                    _result = "success";
                }
                return _result;
            }
        }
        private string _message;
        public string message
        {
            set
            {
                _message = value;
            }
            get
            {
                if (_message == null)
                {
                    _message = "success";
                }
                return _message;
            }
        }

        private T _Data;
        public T Data
        {
            set
            {
                _Data = value;
            }
            get
            {
                if (_Data == null)
                {
                    _Data = GetData();
                }
                return _Data;
            }
        }

    }

    public class BaseModel
    {
        public string current_id { get; set; }
    }
}
