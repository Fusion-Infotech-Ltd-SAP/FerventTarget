using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Target.Model
{
    public class CustomerMaster
    {
        public string custom_customer_code { get; set; }
        public string customer_name { get; set; }
        public string customer_type { get; set; }
        public string customer_group { get; set; }
        public string custom_status { get; set; }
        public string custom_sales_person { get; set; }
        public string custom_start_date { get; set; }
        public string custom_end_date { get; set; }
        public string custom_location_code { get; set; }
        public string custom_location { get; set; }
        public string custom_latitude { get; set; }
        public string custom_longitude { get; set; }
        public string custom_radius { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }

        public string custom_phone { get; set; }
    }
}
