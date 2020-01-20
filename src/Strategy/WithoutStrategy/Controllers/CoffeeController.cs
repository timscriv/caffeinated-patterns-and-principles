using Microsoft.AspNetCore.Mvc;
using StrategyBasic.Core;

namespace StrategyBasic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoffeeController : ControllerBase
    {
        [HttpPost("{brewMethod}")]
        public ActionResult<string> Get(BrewMethod brewMethod = BrewMethod.Drip)
        {
            Beverage beverage;
            switch (brewMethod)
            {
                case BrewMethod.Espresso:
                    beverage = new Beverage
                    {
                        BrewMethod = BrewMethod.Espresso,
                        IsBrewing = true
                    };
                    break;
                case BrewMethod.FrenchPress:
                    beverage = new Beverage
                    {
                        BrewMethod = BrewMethod.FrenchPress,
                        IsBrewing = true
                    };
                    break;
                case BrewMethod.PourOver:
                    beverage = new Beverage
                    {
                        BrewMethod = BrewMethod.PourOver,
                        IsBrewing = true
                    };
                    break;
                case BrewMethod.Drip:
                default:
                    beverage = new Beverage
                    {
                        BrewMethod = BrewMethod.Drip,
                        IsBrewing = true
                    };
                    break;
            }
            return beverage?.ToString();
        }
    }
}
