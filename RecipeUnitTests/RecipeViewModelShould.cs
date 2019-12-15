using System;
using Xunit;
using RecipeUIClassLib.Areas.Recipes.Models;
using Newtonsoft.Json;
using FluentAssertions;
using System.IO;

namespace RecipeUnitTests
{
    public class RecipeViewModelShould
    {
        [Fact]
        public void deserialize_prep_time()
        {
            // Microsoft's JsonSerializer doesn't currently work with Timespan boo :( changed code to use Newtonsoft
            // https://github.com/dotnet/corefx/issues/38641
            var recipe = new RecipeViewModel();
            recipe.Title = "Testipe";
            recipe.PrepTime = "0:13";
            var recipeString = JsonConvert.SerializeObject(recipe);
            var recipeDeserialized = JsonConvert.DeserializeObject<RecipeViewModel>(recipeString);

            recipeDeserialized.Title.Should().Be("Testipe");
            TimeSpan.TryParseExact(recipeDeserialized.PrepTime, "g", null, out var prepTime).Should().BeTrue();
            prepTime.Minutes.Should().Be(13);
        }

        [Fact]
        public void format_prep_time()
        {
            var recipe = new RecipeViewModel();
            recipe.Title = "Testipe";
            recipe.PrepTime = "0:05";
            var recipeString = JsonConvert.SerializeObject(recipe);
            var recipeDeserialized = JsonConvert.DeserializeObject<RecipeViewModel>(recipeString);

            recipeDeserialized.Title.Should().Be("Testipe");
            recipeDeserialized.PrepTime.Should().Be("0:05");
            TimeSpan.TryParseExact(recipeDeserialized.PrepTime, "g", null, out var prepTime).Should().BeTrue();
            prepTime.Minutes.Should().Be(5);
        }

        [Fact]
        public void deserialize_recipe()
        {
            var recipe = JsonConvert.DeserializeObject<RecipeViewModel>(File.ReadAllText("./TestData/Recipe.json"));

            recipe.Title.Should().Be("Garlic Pizza");
            recipe.Description.Should().Be("Best pizza ever!");
            recipe.Instructions.Count.Should().Be(4);
            TimeSpan.TryParseExact(recipe.PrepTime, "g", null, out var prepTime).Should().BeTrue();
            prepTime.Minutes.Should().Be(15);
            TimeSpan.TryParseExact(recipe.CookTime, "g", null, out var cookTime).Should().BeTrue();
            cookTime.Minutes.Should().Be(30);
            cookTime.Hours.Should().Be(1);
            recipe.Keywords.Count.Should().Be(2);
            recipe.Yield.Should().Be("Makes 12 pieces of pizza");
            recipe.Ingredients.Count.Should().Be(3);
            recipe.Preparation.Count.Should().Be(1);
            recipe.Category.Name.Should().Be("Dessert");
            recipe.Id.Should().Be("5ce93843383cf400017b105e");
        }

        [Fact]
        public void get_timespan_for_display_minute()
        {
            var recipe = JsonConvert.DeserializeObject<RecipeViewModel>(File.ReadAllText("./TestData/Recipe.json"));
            recipe.PrepTime = "00:01";

            recipe.PrepTimeDisplay.Should().Be("Prep Time: 1 minute");
        }

        [Fact]
        public void get_timespan_for_display_minutes()
        {
            var recipe = JsonConvert.DeserializeObject<RecipeViewModel>(File.ReadAllText("./TestData/Recipe.json"));
            
            recipe.PrepTimeDisplay.Should().Be("Prep Time: 15 minutes");
        }

        [Fact]
        public void get_timespan_for_display_hour_and_minutes()
        {
            var recipe = JsonConvert.DeserializeObject<RecipeViewModel>(File.ReadAllText("./TestData/Recipe.json"));
            
            recipe.CookTimeDisplay.Should().Be("Cook Time: 1 hour and 30 minutes");
        }

        [Fact]
        public void get_timespan_for_display_hours_and_minutes()
        {
            var recipe = JsonConvert.DeserializeObject<RecipeViewModel>(File.ReadAllText("./TestData/Recipe.json"));
            recipe.CookTime = "02:30";
            
            recipe.CookTimeDisplay.Should().Be("Cook Time: 2 hours and 30 minutes");
        }
    }
}