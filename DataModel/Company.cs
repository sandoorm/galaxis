using System.Collections.Generic;

namespace GalaxisProjectWebAPI.DataModel
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string RegistrationNumber { get; set; }
        public string vatNumber { get; set; }
        public string OfficialEmailAddress { get; set; }
        public string ContactPersonName { get; set; }
        public string Position { get; set; }
        public string ContactPersonPhoneNumber { get; set; }
        public string ContactPersonEmail { get; set; }

        public ICollection<Fund> Funds { get; set; }
    }
}