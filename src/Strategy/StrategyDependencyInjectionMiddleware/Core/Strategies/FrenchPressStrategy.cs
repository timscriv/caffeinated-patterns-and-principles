namespace StrategyBasic.Core.Strategies
{
    public class FrenchPressStrategy : ICoffeeStrategy
    {
        public Beverage Brew()
        {
            return new Beverage
            {
                BrewMethod = BrewMethod.FrenchPress,
                IsBrewing = true
            };
        }
    }
}
