namespace StrategyBasic.Core
{
    public interface ICoffeeStrategy
    {
        BrewMethod Method { get; }
        Beverage Brew();
    }
}
