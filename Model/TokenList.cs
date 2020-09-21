using System.Collections.Generic;

namespace GalaxisProjectWebAPI.Model
{
    public class TokenList
    {
        public List<TokenInfo> TokenInfos { get; set; }

        public TokenList()
        {
            TokenInfos = new List<TokenInfo>();
        }
    }
}