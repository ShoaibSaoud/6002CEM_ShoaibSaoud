using System;
using System.Collections.Generic;

namespace TheRecipeApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string ImageType { get; set; }
        public string Title { get; set; }
        public int ReadyInMinutes { get; set; }
        public int Servings { get; set; }
        public string SourceUrl { get; set; }
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
        public bool GlutenFree { get; set; }
        public bool DairyFree { get; set; }
        public bool VeryHealthy { get; set; }
        public bool Cheap { get; set; }
        public bool VeryPopular { get; set; }
        public bool Sustainable { get; set; }
        public bool LowFodmap { get; set; }
        public int WeightWatcherSmartPoints { get; set; }
        public string Gaps { get; set; }
        public object PreparationMinutes { get; set; }
        public object CookingMinutes { get; set; }
        public int AggregateLikes { get; set; }
        public float HealthScore { get; set; }
        public string CreditsText { get; set; }
        public object License { get; set; }
        public string SourceName { get; set; }
        public float PricePerServing { get; set; }
        public List<ExtendedIngredient> ExtendedIngredients { get; set; }
        public string Summary { get; set; }
        public List<string> DishTypes { get; set; }
        public string Instructions { get; set; }
        public List<AnalyzedInstruction> AnalyzedInstructions { get; set; }
        public object OriginalId { get; set; }
        public float SpoonacularScore { get; set; }
        public string SpoonacularSourceUrl { get; set; }
        public string FirstLetter => !string.IsNullOrEmpty(SourceName) ? SourceName[0].ToString().ToUpper() : "N";

    }

    public class ExtendedIngredient
    {
        public int Id { get; set; }
        public string Aisle { get; set; }
        public string Image { get; set; }
        public string Consistency { get; set; }
        public string Name { get; set; }
        public string NameClean { get; set; }
        public string Original { get; set; }
        public string OriginalName { get; set; }
        public float Amount { get; set; }
        public string Unit { get; set; }
        public List<string> Meta { get; set; }
        public Measures Measures { get; set; }
    }

    public class Measures
    {
        public Us Us { get; set; }
        public Metric Metric { get; set; }
    }

    public class Us
    {
        public float Amount { get; set; }
        public string UnitShort { get; set; }
        public string UnitLong { get; set; }
    }

    public class Metric
    {
        public float Amount { get; set; }
        public string UnitShort { get; set; }
        public string UnitLong { get; set; }
    }

    public class AnalyzedInstruction
    {
        public string Name { get; set; }
        public List<Step> Steps { get; set; }
    }

    public class Step
    {
        public int Number { get; set; }
        public string StepText { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Equipment> Equipment { get; set; }
        public Length Length { get; set; }
    }

    public class Length
    {
        public int Number { get; set; }
        public string Unit { get; set; }
    }

    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LocalizedName { get; set; }
        public string Image { get; set; }
    }

    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LocalizedName { get; set; }
        public string Image { get; set; }
    }
}
