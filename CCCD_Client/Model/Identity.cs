using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCD_Client.Model
{
    class Identity
    {
        private String IdNumber;
        private String OldIdNumber;
        private String FullName;
        private int Sex;
        private String Ethnic;
        private String Religion;
        private String Identification;
        private DateTime DateOfBirth;
        private DateTime DateIssue;
        private DateTime? DateExpired;
        private String PlaceOfOrigin;
        private String PlaceOfResidence;
        private String Father;
        private String Mother;
        private String Mate;
        private String PortraitImage;
        private DateTime DateScan;

        public Identity(string idNumber, string oldIdNumber, string fullName, int sex, string ethnic, string religion, string identification, DateTime dateOfBirth, DateTime dateIssue, DateTime? dateExpired, string placeOfOrigin, string placeOfResidence, string father, string mother, string mate, string portraitImage, DateTime dateScan)
        {
            this.IdNumber = idNumber;
            this.OldIdNumber = oldIdNumber;
            this.FullName = fullName;
            this.Sex = sex;
            this.Ethnic = ethnic;
            this.Religion = religion;
            this.Identification = identification;
            this.DateOfBirth = dateOfBirth;
            this.DateIssue = dateIssue;
            this.DateExpired = dateExpired;
            this.PlaceOfOrigin = placeOfOrigin;
            this.PlaceOfResidence = placeOfResidence;
            this.Father = father;
            this.Mother = mother;
            this.Mate = mate;
            this.PortraitImage = portraitImage;
            this.DateScan = dateScan;
        }

        public Identity()
        {

        }

        public string idNumber { get => IdNumber; set => IdNumber = value; }
        public string oldIdNumber { get => OldIdNumber; set => OldIdNumber = value; }
        public string fullName { get => FullName; set => FullName = value; }
        public int sex { get => Sex; set => Sex = value; }
        public string ethnic { get => Ethnic; set => Ethnic = value; }
        public string religion { get => Religion; set => Religion = value; }
        public string identification { get => Identification; set => Identification = value; }
        public DateTime dateOfBirth { get => DateOfBirth; set => DateOfBirth = value; }
        public DateTime dateIssue { get => DateIssue; set => DateIssue = value; }
        public DateTime? dateExpired { get => DateExpired; set => DateExpired = value; }
        public string placeOfOrigin { get => PlaceOfOrigin; set => PlaceOfOrigin = value; }
        public string placeOfResidence { get => PlaceOfResidence; set => PlaceOfResidence = value; }
        public string father { get => Father; set => Father = value; }
        public string mother { get => Mother; set => Mother = value; }
        public string mate { get => Mate; set => Mate = value; }
        public string portraitImage { get => PortraitImage; set => PortraitImage = value; }
        public DateTime dateScan { get => DateScan; set => DateScan = value; }

    }
}
