namespace GalaxisProjectWebAPI.Model.TokenPriceHistoryProvider
{
    public interface ITokenPriceHistoryProvider
    {
        double GetCurrentPrice(string tokenSymbol);
    }
}