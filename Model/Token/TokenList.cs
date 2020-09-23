using System.Collections.Generic;

namespace GalaxisProjectWebAPI.Model.Token
{
    public class TokenList<T> where T : class
    {
        public List<T> TokenInfos { get; set; }

        public TokenList(List<T> tokens = null)
        {
            TokenInfos = tokens == null
                ? new List<T>()
                : tokens;
        }
    }
}