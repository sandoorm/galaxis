using System.Threading.Tasks;

namespace GalaxisProjectWebAPI.Model.FundPerformanceCalculation
{
    public interface IFundPerformanceCalculator
    {
        Task<FundPerformance> CalculateFundPerformance(int fundId, string timeStampFrom, string timeStampTo);
    }
}