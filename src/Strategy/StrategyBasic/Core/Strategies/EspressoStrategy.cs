namespace StrategyBasic.Core.Strategies
{
    public class EspressoStrategy : ICoffeeStrategy
    {
        public Beverage Brew()
        {
            return new Beverage
            {
                BrewMethod = BrewMethod.Espresso,
                IsBrewing = true
            };
        }
    }
}
