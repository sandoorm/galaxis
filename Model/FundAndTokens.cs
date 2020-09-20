using System.Collections.Generic;

using GalaxisProjectWebAPI.DataModel;

namespace GalaxisProjectWebAPI.Model
{
    public class FundAndTokens
    {
        public string FundName { get; set; }
        public string FundManagerName { get; set; }
        public List<Token> Tokens { get; set; }
    }
}