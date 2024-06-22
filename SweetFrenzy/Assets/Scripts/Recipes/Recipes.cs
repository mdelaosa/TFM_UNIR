using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipes : MonoBehaviour
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

    #region Getters and setters
    public static Dictionary<RecipeName, Dictionary<string, object>> GetRecipes()
    {
        return recipes;
    }
    #endregion
}
