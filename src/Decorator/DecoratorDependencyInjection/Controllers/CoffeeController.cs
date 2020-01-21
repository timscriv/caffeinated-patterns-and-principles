using DecoratorDependencyInjection.Core;
using Microsoft.AspNetCore.Mvc;

namespace DecoratorDependencyInjection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeMaker _coffeeMaker;

        public CoffeeController(ICoffeeMaker coffeeMaker)
        {
            _coffeeMaker = coffeeMaker;
        }

        [HttpGet]
        public ActionResult<Coffee> Get()
        {
            try
            {
                return _coffeeMaker.OrderCoffee();
            }
            catch (OutOfCoffeeException)
            {
                return BadRequest("No coffee left, brew a new pot.");
            }
        }
    }
}
