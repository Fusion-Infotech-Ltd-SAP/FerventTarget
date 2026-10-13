using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Target.Model
{
    // Mobile App API Response
    public class DeliveryReturnData
    {
        public int status { get; set; }
        public string message { get; set; }
    }

    // ERPNext API Response
    public class DeliveryERPNextReturnData
    {
        public MessageData message { get; set; }

        public class MessageData
        {
            public string status { get; set; }
            public string name { get; set; }
        }
    }
}
