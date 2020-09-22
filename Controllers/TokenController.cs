using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using GalaxisProjectWebAPI.Model;

namespace GalaxisProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController
    {
        private readonly IFundRepository fundRepository;

        public TokenController(IFundRepository fundRepository)
        {
            this.fundRepository = fundRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DataModel.Token>> GetAllTokens()
        {
            return new List<DataModel.Token>
            {
                new DataModel.Token
                {
                    Name = "Ethereum",
                    Symbol = "ETH",
                    Decimals = 18
                },
                new DataModel.Token
                {
                    Name = "Bitcoin",
                    Symbol = "BTC",
                    Decimals = 18
                }
            };

            //IEnumerable<Model.Fund> allDbFunds = this.fundRepository.GetAllFundsAsync();
            //allDbFunds.Concat(staticFunds.Select(fund => new Model.Fund(fund)).ToList()).ToList();
            //List<Model.Fund> list = allDbFunds.ToList();
            //return Ok(allDbFunds);
        }

        [HttpGet("{symbol}")]
        public ActionResult<DataModel.Token> GetTokenBySymbol(string symbol)
        {
            if (symbol == "ETH")
            {
                return new DataModel.Token
                {
                    Name = "Ethereum",
                    Symbol = "ETH",
                    Decimals = 18
                };
            }
            else if (symbol == "BTC")
            {
                return new DataModel.Token
                {
                    Name = "Bitcoin",
                    Symbol = "BTC",
                    Decimals = 18
                };
            }

            return null;
        }

        [HttpPost]
        public int AddToken()
        {
            return 1;
        }

        [HttpPut("{id}")]
        public int ModifyToken(int id)
        {
            return 3;
        }

        [HttpDelete("{id}")]
        public int DeleteFund(int id)
        {
            return 3;
        }
    }
}