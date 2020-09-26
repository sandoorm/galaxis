using System.Collections.Generic;

using GalaxisProjectWebAPI.Model.Token;

namespace GalaxisProjectWebAPI.ApiModel
{
    public class FundTokenCreateRequest
    {
        public List<TokenAllocationInfo> FundTokenAllocations { get; set; }
    }
}