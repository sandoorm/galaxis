using System.Collections.Generic;

namespace GalaxisProjectWebAPI.DataModel
{
    public class Company
    {
        public int ID { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public ICollection<Fund> Funds { get; set; }
    }
}