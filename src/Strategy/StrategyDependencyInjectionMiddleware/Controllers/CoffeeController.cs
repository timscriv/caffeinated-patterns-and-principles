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
        private readonly ICoffeeStrategy _coffeeStrategy;

        public CoffeeController(ICoffeeStrategy coffeeStrategy)
        {
            _coffeeStrategy = coffeeStrategy;
        }

        [HttpPost]
        public ActionResult<string> Get()
        {
            return _coffeeStrategy.Brew().ToString();
        }
    }
}
