using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCD_Client.Model
{
    public class ScanIdViewModel
    {
        public String dataId { get; set; }
        public String idNumber { get; set; }
        public String fullName { get; set; }
        public String sex { get; set; }
        public String dateOfBirth { get; set; }
        public String dateIssue { get; set; }
        public String dateExpired { get; set; }
        public String placeOfOrigin { get; set; }
        public String placeOfResidence { get; set; }
    }
}
