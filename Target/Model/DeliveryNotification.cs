using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Target.Model
{
    public class DeliveryNotification
    {
        // Mobile App API Response
        public string user_id { get; set; }
        public string message { get; set; }


        // ERPNext API Response
        public class DeliveryChallanNotification
        {
            public string type { get; set; }
            public string greetings { get; set; }
            public string refchalan { get; set; }
            public string date { get; set; }
            public string vehicle_no { get; set; }
            public string driver_contact { get; set; }

            public string dealer_code { get; set; }
            public List<Item> items { get; set; }

            public class Item
            {
                public string item_description { get; set; }
                public decimal qty { get; set; }
            }
        }

    }
}
