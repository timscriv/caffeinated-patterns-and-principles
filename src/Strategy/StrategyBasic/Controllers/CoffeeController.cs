using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StrategyBasic.Core;
using StrategyBasic.Core.Strategies;

namespace StrategyBasic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeContext _coffeeContext;

        public CoffeeController(ICoffeeContext coffeeContext)
        {
            _coffeeContext = coffeeContext;
        }

        [HttpPost("{brewMethod}")]
        public ActionResult<string> Get(BrewMethod brewMethod = BrewMethod.Drip)
        {
            switch (brewMethod)
            {
                case BrewMethod.Espresso:
                    _coffeeContext.SetCoffeeStrategy(new EspressoStrategy());
                    break;
                case BrewMethod.FrenchPress:
                    _coffeeContext.SetCoffeeStrategy(new FrenchPressStrategy());
                    break;
                case BrewMethod.PourOver:
                    _coffeeContext.SetCoffeeStrategy(new PourOverStrategy());
                    break;
                case BrewMethod.Drip:
                default:
                    _coffeeContext.SetCoffeeStrategy(new DripStrategy());
                    break;
            }
            return _coffeeContext.Brew().ToString();
        }
    }
}
