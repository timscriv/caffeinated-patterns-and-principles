namespace StrategyBasic.Core.Strategies
{
    public class EspressoStrategy : ICoffeeStrategy
    {
        public BrewMethod Method => BrewMethod.Espresso;
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
