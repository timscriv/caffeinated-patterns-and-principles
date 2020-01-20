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
            _coffeeContext.SetCoffeeStrategy(brewMethod);

            return _coffeeContext.Brew().ToString();
        }
    }
}
