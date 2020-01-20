using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BuilderBasic.Core
{
    public class Beverage
    {
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Ingredient
    {
        Cinnamon,
        Chocolate,
        Coffee,
        EspressoShot,
        MilkFoam,
        SteamedMilk,
        SugarPump,
        VanillaPump,
        Water,
        Whiskey,
        WhippedCream
    }
}
