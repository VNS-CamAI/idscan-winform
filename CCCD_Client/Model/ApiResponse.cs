using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCD_Client.Model
{
    public class ApiResponse
    {
        public string page { get; set; }
        public string limit { get; set; }
        public List<ScanIdModel> data { get; set; }
        public string total { get; set; }
    }
}
