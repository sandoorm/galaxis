using System.Threading.Tasks;

namespace GalaxisProjectWebAPI.Model.FundPerformanceCalculation
{
    public interface IFundPerformanceCalculator
    {
        Task<FundPerformance> CalculateFundPerformance(string fundAddress, int intervalInDays);
    }
}