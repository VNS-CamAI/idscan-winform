using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCD_Client.Model
{
    public class ScanIdModel
    {
        public String dataId { get; set; }
        public String idNumber{ get; set; }
        public String oldIdNumber{ get; set; }
        public String fullName{ get; set; }
        public int sex{ get; set; }
        public String ethnic{ get; set; }
        public String religion{ get; set; }
        public String identification{ get; set; }
        public DateTime dateOfBirth{ get; set; }
        public DateTime dateIssue{ get; set; }
        public DateTime? dateExpired{ get; set; }
        public String placeOfOrigin{ get; set; }
        public String placeOfResidence{ get; set; }
        public String father{ get; set; }
        public String mother{ get; set; }
        public String mate{ get; set; }
        public String portraitImage{ get; set; }
        public DateTime dateScan{ get; set; }
        public String frontId{ get; set; }
        public String backId{ get; set; }
        public int status{ get; set; }
        public int userId{ get; set; }
    }
}
