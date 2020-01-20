using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StrategyBasic.Core
{
    public class CoffeeContext : ICoffeeContext
    {
        private readonly IEnumerable<Lazy<ICoffeeStrategy, BrewMethod>> _coffeeStrategies;
        private ICoffeeStrategy _coffeeStrategy;

        public CoffeeContext(IEnumerable<Lazy<ICoffeeStrategy, BrewMethod>> coffeeStrategies)
        {
            _coffeeStrategies = coffeeStrategies;
        }

        public Beverage Brew()
        {
            //In real-world scenario this method would probably not match up and shared logic
            //could be placed here
            return _coffeeStrategy.Brew();
        }

        public void SetCoffeeStrategy(BrewMethod method)
        {
            _coffeeStrategy = _coffeeStrategies.FirstOrDefault(c=> c.Metadata == method)?.Value ?? throw new ArgumentException($"Invalid {nameof(method)}");
        }
    }

}
