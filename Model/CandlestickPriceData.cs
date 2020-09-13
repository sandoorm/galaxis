using System;

namespace GalaxisProjectWebAPI.Model
{
    public class CandlestickPriceData
    {
        public string TimeStamp { get; set; }
        public double Price { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
    }
}