using System.Threading.Tasks;

using GalaxisProjectWebAPI.ApiModel;
using GalaxisProjectWebAPI.Model.Token;

namespace GalaxisProjectWebAPI.Model
{
    public interface ITokenRepository
    {
        Task<TokenList<TokenInfo>> GetAllTokensAsync();

        Task AddTokenAsync(TokenCreationRequest tokenCreationRequest);
    }
}