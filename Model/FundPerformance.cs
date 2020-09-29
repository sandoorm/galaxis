using System.Collections.Generic;

namespace GalaxisProjectWebAPI.Model
{
    public class FundPerformance
    {
        public Dictionary<uint, double> FundValuesByTimeStamps { get; set; }
    }
}