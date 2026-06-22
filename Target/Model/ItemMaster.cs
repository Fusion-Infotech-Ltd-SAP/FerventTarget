using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Target.Model
{
    public class ItemMaster
    {
        public string item_code { get; set; }
        public string item_name { get; set; }
        public string item_group { get; set; }
        public string custom_inventory_uom { get; set; }
        public string custom_on_hand { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
    }
}
