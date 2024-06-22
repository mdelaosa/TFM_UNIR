using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeGenerator : MonoBehaviour
{
    private static Dictionary<RecipeName, Dictionary<string, object>> recipes = new Dictionary<RecipeName, Dictionary<string, object>>()
    {
        {
            RecipeName.applePie, new Dictionary<string, object>
            {
                { "ingredients", new List<FoodName> { FoodName.apple, FoodName.milk, FoodName.flour, FoodName.egg } },
                { "deliveryTime", 5 },
                { "points", 10 }
            }
        },
        {
            RecipeName.fruitBowl, new Dictionary<string, object>
            {
                { "ingredients", new List<FoodName> { FoodName.strawberry, FoodName.apple, FoodName.banana } },
                { "deliveryTime", 3 },
                { "points", 10 }
            }
        },
        {
            RecipeName.fruitSmoothie, new Dictionary<string, object>
            {
                { "ingredients", new List<FoodName> { FoodName.strawberry, FoodName.banana, FoodName.milk } },
                { "deliveryTime", 4 },
                { "points", 10 }
            }
        }
    };

    public void GenerateRecipe()
    {
        RecipeName recipeName = GetRandomRecipeName();
        GameObject recipeObject = new GameObject("Recipe_" + recipeName);
        Recipe recipe = Recipe.CreateRecipe(recipeObject, recipeName);

        Debug.Log("Ingredientes para la receta de " + recipeName + ": " + string.Join(", ", recipe.GetIngredients()));
        Debug.Log("Tiempo de entrega para la receta: " + recipe.GetDeliveryTime() + " segundos");
    }

    private RecipeName GetRandomRecipeName()
    {
        Array values = Enum.GetValues(typeof(RecipeName));
        System.Random random = new System.Random();
        RecipeName randomRecipeName = (RecipeName)values.GetValue(random.Next(values.Length));
        return randomRecipeName;
    }

    #region Getters and setters
    public static Dictionary<RecipeName, Dictionary<string, object>> GetRecipes()
    {
        return recipes;
    }
    #endregion
}
