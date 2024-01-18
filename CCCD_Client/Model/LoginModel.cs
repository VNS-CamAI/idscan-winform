using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCD_Client.Model
{
    public class LoginModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
        public int status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string data { get; set; }
        public string action { get; set; }
        public List<string> roles { get; set; }
        public string accessToken { get; set; }
        public string tokenType { get; set; }
    }
}
