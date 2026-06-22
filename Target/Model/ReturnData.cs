using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Target.Model
{
    public class ReturnData
    {
        public string data { get; set; }
        public string ReturnMsg { get; set; }

        public string status { get; set; }
        public string message { get; set; }

        //For Item Master
        public string item_code { get; set; }

        //For Cost center
        public string cost_center_name { get; set; }

        //For Item Group
        public string item_group_name { get; set; }

    }
}
