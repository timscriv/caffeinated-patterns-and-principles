namespace BuilderBasic.Core
{
    public class BeverageBuilder
    {
        private Beverage _beverage;

        public static BeverageBuilder Americano(string orderName) =>
            OrderFor(orderName)
            .AddIngredient(Ingredient.EspressoShot)
            .AddIngredient(Ingredient.Water);

        public static BeverageBuilder Cappuccino(string orderName) =>
            OrderFor(orderName)
            .AddIngredient(Ingredient.EspressoShot)
            .AddIngredient(Ingredient.SteamedMilk)
            .AddIngredient(Ingredient.MilkFoam);

        public static BeverageBuilder ConPanna(string orderName) =>
            OrderFor(orderName)
            .AddIngredient(Ingredient.EspressoShot)
            .AddIngredient(Ingredient.WhippedCream);

        public static BeverageBuilder Coffee(string orderName) =>
            OrderFor(orderName)
            .AddIngredient(Ingredient.Coffee);

        public static BeverageBuilder Doppio(string orderName) =>
            OrderFor(orderName)
            .AddIngredient(Ingredient.EspressoShot)
            .AddIngredient(Ingredient.EspressoShot);

        public static BeverageBuilder Espresso(string orderName) =>
            OrderFor(orderName)
            .AddIngredient(Ingredient.EspressoShot);

        public static BeverageBuilder Mocha(string orderName) =>
            OrderFor(orderName)
            .AddIngredient(Ingredient.EspressoShot)
            .AddIngredient(Ingredient.Chocolate)
            .AddIngredient(Ingredient.SteamedMilk)
            .AddIngredient(Ingredient.WhippedCream);

        public static BeverageBuilder OrderFor(string orderName) => new BeverageBuilder()
            .SetOrderName(orderName);

        public BeverageBuilder()
        {
            this.Reset();
        }

        public BeverageBuilder Reset()
        {
            _beverage = new Beverage();
            return this;
        }

        public BeverageBuilder SetOrderName(string orderName)
        {
            _beverage.Name = orderName;
            return this;
        }

        public BeverageBuilder AddIngredient(Ingredient ingredient)
        {
            _beverage.Ingredients.Add(ingredient);
            return this;
        }

        public Beverage Build()
        {
            var order = _beverage;
            this.Reset();
            return order;
        }


    }
}
