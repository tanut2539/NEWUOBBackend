using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOB.PVD.Model
{
    public class SmkFileDTO
    {
        public string smk_file_seq { get; set; }
        public string smk_title_th { get; set; }
        public string smk_title_en { get; set; }
        public string smk_file_name_th { get; set; }
        public string smk_file_name_th_url { get; set; }
        public string smk_file_original_name_th { get; set; }
        public string smk_file_name_en { get; set; }
        public string smk_file_name_en_url { get; set; }
        public string smk_file_original_name_en { get; set; }
        public string smk_type { get; set; }
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string modify_by { get; set; }
        public string modify_date { get; set; }
        public string order_by { get; set; }
        public string active { get; set; }

    }
}
