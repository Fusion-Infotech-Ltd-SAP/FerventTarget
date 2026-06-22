using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Target.Model
{
    public class ItemGroupMaster
    {
        public string item_group_name { get; set; }
        public string custom_item_catagory_code { get; set; }
        public string parent_item_group { get; set; }
        public string custom_item_catagory_name { get; set; }
        public string custom_item_group_code { get; set; }
        public string is_group { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
    }
}
