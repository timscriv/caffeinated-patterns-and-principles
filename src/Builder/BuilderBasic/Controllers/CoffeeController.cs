using BuilderBasic.Core;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BuilderBasic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoffeeController : ControllerBase
    {
        public CoffeeController()
        {
        }

        [HttpGet("{beverage}")]
        public Beverage Get(string beverage,
            [FromQuery] string name = "",
            [FromQuery] bool withExtraShot = false)
        {
            var beverageBuilder = (beverage.ToLower()) switch
            {
                "americano" => BeverageBuilder.Americano(name),
                "cappuccino" => BeverageBuilder.Cappuccino(name),
                "conpanna" => BeverageBuilder.ConPanna(name),
                "coffee" => BeverageBuilder.Coffee(name),
                "doppio" => BeverageBuilder.Doppio(name),
                "espresso" => BeverageBuilder.Espresso(name),
                "mocha" => BeverageBuilder.Mocha(name),
                _ => throw new ArgumentException("That's not on the menu."),
            };

            if (withExtraShot)
            {
                beverageBuilder.AddIngredient(Ingredient.EspressoShot);
            }

            return beverageBuilder.Build();
        }
    }
}
