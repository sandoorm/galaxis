using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.Model;
using GalaxisProjectWebAPI.Model.Token;

namespace GalaxisProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController
    {
        private readonly ITokenRepository tokenRepository;

        public TokenController(ITokenRepository tokenRepository)
        {
            this.tokenRepository = tokenRepository;
        }

        [HttpGet("GetAllTokens")]
        public async Task<ActionResult<TokenList<TokenInfo>>> GetAllTokens()
        {
            return await this.tokenRepository.GetAllTokensAsync();
        }

        [HttpGet("{symbol}/GetToken")]
        public async Task<ActionResult<TokenInfo>> GetTokenBySymbol(string symbol)
        {
            var relevantTokens = await this.tokenRepository
                .GetAllTokensAsync();

            return relevantTokens
                .TokenInfos
                .FirstOrDefault(tokenInfo => tokenInfo.Symbol == symbol);
        }

        [HttpPost("Add")]
        public async Task AddToken([FromBody] TokenCreationRequest tokenCreationRequest)
        {
            var relevantTokens = await this.tokenRepository.GetAllTokensAsync();

            bool alreadyCreated = relevantTokens
                .TokenInfos
                .Any(tokenInfo => tokenInfo.Symbol == tokenCreationRequest.Symbol);

            if (!alreadyCreated)
            {
                await this.tokenRepository.AddTokenAsync(tokenCreationRequest);
            }
        }
    }
}