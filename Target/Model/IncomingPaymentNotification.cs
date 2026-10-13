using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Target.Model
{
    public class IncomingPaymentNotification
    {
        public string user_id { get; set; }
        public string message { get; set; }

        public class IncomingPaymentERPNextNotification
        {
            public string type { get; set; }
            public string greetings { get; set; }
            public string refchalan { get; set; }
            public string dealer_code { get; set; }
            public string date { get; set; }
            public decimal amount { get; set; }
        }
    }
}
