using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Target.Model
{
    // Mobile App Response
    public class IncomingPaymentReturnData
    {
        public int status { get; set; }
        public string message { get; set; }
    }

    // ERPNext Response
    public class IncomingPaymentERPNextReturnData
    {
        public MessageData message { get; set; }

        public class MessageData
        {
            public string status { get; set; }
            public string name { get; set; }
        }
    }
}
