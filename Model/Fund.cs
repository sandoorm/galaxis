namespace GalaxisProjectWebAPI.Model
{
    public class Fund
    {
        public int ID { get; set; }

        public int CompanyId { get; set; }

        public string FundName { get; set; }

        public string InvestmentFundManagerName { get; set; }

        public double FloorLevel { get; set; }
    }
}