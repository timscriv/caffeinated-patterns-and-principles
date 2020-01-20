namespace StrategyBasic.Core
{
    public interface ICoffeeContext
    {
        void SetCoffeeStrategy(ICoffeeStrategy strategy);
        Beverage Brew();
    }
}