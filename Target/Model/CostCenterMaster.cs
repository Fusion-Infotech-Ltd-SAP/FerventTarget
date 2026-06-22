using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Target.Model
{
    public class CostCenterMaster
    {
        public string cost_center_name { get; set; }
        public string cost_center_number { get; set; }
        public string custom_type { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }

        public int disabled { get; set; }

    }
}
