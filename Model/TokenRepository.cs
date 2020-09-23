using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.Infrastructure;
using GalaxisProjectWebAPI.Model.Token;
using GalaxisProjectWebAPI.ApiModel;

using DataModelToken = GalaxisProjectWebAPI.DataModel.Token;

namespace GalaxisProjectWebAPI.Model
{
    public class TokenRepository : ITokenRepository
    {
        private readonly GalaxisDbContext galaxisContext;

        public TokenRepository(GalaxisDbContext galaxisContext)
        {
            this.galaxisContext = galaxisContext;
        }

        public async Task<TokenList<TokenInfo>> GetAllTokensAsync()
        {
            var tokens = await this.galaxisContext.Tokens.ToListAsync();
            var tokenInfos = tokens.Select(token => new TokenInfo
            {
                Id = token.Id,
                Name = token.Name,
                Decimals = token.Decimals,
                Symbol = token.Symbol
            })
                .ToList();

            return new TokenList<TokenInfo>(tokenInfos);
        }

        public async Task AddTokenAsync(TokenCreationRequest tokenCreationRequest)
        {
            var token = new DataModelToken
            {
                Symbol = tokenCreationRequest.Symbol,
                Name = tokenCreationRequest.Name,
                Decimals = tokenCreationRequest.Decimals,
                FundTokens = new List<DataModel.FundToken>()
            };

            this.galaxisContext.Tokens.Add(token);
            await this.galaxisContext.SaveChangesAsync();
        }
    }
}