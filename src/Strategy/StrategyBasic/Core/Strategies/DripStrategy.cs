namespace StrategyBasic.Core.Strategies
{
    public class DripStrategy : ICoffeeStrategy
    {
        public Beverage Brew()
        {
            return new Beverage
            {
                BrewMethod = BrewMethod.Drip,
                IsBrewing = true
            };
        }
    }
}
