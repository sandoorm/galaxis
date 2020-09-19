namespace GalaxisProjectWebAPI.ApiModel
{
    public class FundCreateRequest
    {
        public string FundName { get; set; }
        public string InvestmentFundManagerName { get; set; }
        public double FloorLevel { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
    }
}