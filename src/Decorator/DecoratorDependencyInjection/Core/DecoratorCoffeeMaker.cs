namespace DecoratorDependencyInjection.Core
{
    public class DecoratorCoffeeMaker : ICoffeeMaker
    {
        private static int CupsLeft = 10; //not a good way to get this info
        private readonly ICoffeeMaker _coffeeMaker;

        public DecoratorCoffeeMaker(ICoffeeMaker coffeeMaker)
        {
            _coffeeMaker = coffeeMaker;
        }
        public Coffee OrderCoffee()
        {
            if (CupsLeft > 0)
            {
                CupsLeft--;
                return _coffeeMaker.OrderCoffee();
            }

            throw new OutOfCoffeeException();
        }
    }
}
