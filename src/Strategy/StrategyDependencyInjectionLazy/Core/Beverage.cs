namespace StrategyBasic.Core
{
    public class Beverage
    {
        public BrewMethod BrewMethod { get; set; }
        public bool IsBrewing { get; set; }

        public override string ToString()
        {
            if (IsBrewing)
            {
                return $"Currently brewing a {BrewMethod} coffee.";
            }
            else
            {
                return $"Finished brewing a {BrewMethod} coffee.";
            }
        }
    }

    public enum BrewMethod
    {
        Drip,
        Espresso,
        FrenchPress,
        PourOver
    }
}
