namespace GalaxisProjectWebAPI.ApiModel
{
    public class TokenCreationRequest
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int Decimals { get; set; }
    }
}