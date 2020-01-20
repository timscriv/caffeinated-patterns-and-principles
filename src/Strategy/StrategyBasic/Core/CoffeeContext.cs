using System;

namespace StrategyBasic.Core
{
    public class CoffeeContext : ICoffeeContext
    {
        private ICoffeeStrategy _coffeeStrategy;

        public Beverage Brew()
        {
            //In real-world scenario this method would probably not match up and shared logic
            //could be placed here
            return _coffeeStrategy.Brew();
        }

        public void SetCoffeeStrategy(ICoffeeStrategy coffeeStrategy)
        {
            _coffeeStrategy = coffeeStrategy;
        }
    }

}
