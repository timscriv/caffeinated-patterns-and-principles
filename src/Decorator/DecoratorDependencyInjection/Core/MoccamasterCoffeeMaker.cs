namespace DecoratorDependencyInjection.Core
{
    public class MoccamasterCoffeeMaker : ICoffeeMaker
    {
        public MoccamasterCoffeeMaker()
        {

        }
        public Coffee OrderCoffee()
        {
            return new Coffee
            {
                Status = "Here's your coffee"
            };
        }
    }
}
