namespace StrategyBasic.Core.Strategies
{
    public class PourOverStrategy : ICoffeeStrategy
    {
        public BrewMethod Method => BrewMethod.PourOver;
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
