namespace StrategyBasic.Core.Strategies
{
    public class PourOverStrategy : ICoffeeStrategy
    {
        public Beverage Brew()
        {
            return new Beverage
            {
                BrewMethod = BrewMethod.PourOver,
                IsBrewing = true
            };
        }
    }
}
