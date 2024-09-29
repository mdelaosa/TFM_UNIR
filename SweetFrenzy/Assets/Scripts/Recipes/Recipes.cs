using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipes : MonoBehaviour
{
    private static Dictionary<RecipeName, Dictionary<string, object>> recipes = new Dictionary<RecipeName, Dictionary<string, object>>()
    {
        {
            RecipeName.applePie, new Dictionary<string, object>
            {
                { "ingredients", new List<FoodName> { FoodName.apple, FoodName.milk, FoodName.flour, FoodName.egg } },
                { "deliveryTime", 70 },
                { "points", 50 },
                { "image", Resources.Load<Sprite>("Sprites/applePieIcon") }
            }
        },
        {
            RecipeName.fruitBowl, new Dictionary<string, object>
            {
                { "ingredients", new List<FoodName> { FoodName.strawberry, FoodName.apple, FoodName.banana } },
                { "deliveryTime", 40 },
                { "points", 10 },
                { "image", Resources.Load<Sprite>("Sprites/fruitBowlIcon") }
            }
        },
        {
            RecipeName.fruitSmoothie, new Dictionary<string, object>
            {
                { "ingredients", new List<FoodName> { FoodName.strawberry, FoodName.banana, FoodName.milk } },
                { "deliveryTime", 60 },
                { "points", 20 },
                { "image", Resources.Load<Sprite>("Sprites/smoothieIcon")}
            }
        }
    };

    void Start()
    {
        // Puedes eliminar este método si no lo necesitas
    }

    #region Getters and setters
    public static Dictionary<RecipeName, Dictionary<string, object>> GetRecipes()
    {
        return recipes;
    }
    #endregion
}
