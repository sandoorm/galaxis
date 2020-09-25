namespace GalaxisProjectWebAPI.ApiModel
{
    public class FundCreateRequest
    {
        public string FundName { get; set; }
        public string InvestmentFundManagerName { get; set; }
        public double FloorLevel { get; set; }
        public int CompanyId { get; set; }
    }
}