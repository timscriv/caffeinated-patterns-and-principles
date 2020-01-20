namespace StrategyBasic.Core
{
    public interface ICoffeeContext
    {
        void SetCoffeeStrategy(BrewMethod method);
        Beverage Brew();
    }
}