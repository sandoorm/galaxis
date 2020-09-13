namespace GalaxisProjectWebAPI.DataModel
{
    public class Fund
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string FundName { get; internal set; }
        public string InvestmentFundManagerName { get; internal set; }
        public double FloorLevel { get; internal set; }

        public Company Company { get; set; }
    }
}