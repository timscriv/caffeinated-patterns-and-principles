namespace StrategyBasic.Core.Strategies
{
    public class DripStrategy : ICoffeeStrategy
    {
        public BrewMethod Method => BrewMethod.Drip;

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
