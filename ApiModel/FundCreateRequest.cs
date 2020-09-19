namespace GalaxisProjectWebAPI.ApiModel
{
    public class FundCreateRequest
    {
        public string FundName { get; internal set; }
        public string InvestmentFundManagerName { get; internal set; }
        public double FloorLevel { get; internal set; }
        public string Address { get; internal set; }
        public string CompanyName { get; internal set; }
    }
}