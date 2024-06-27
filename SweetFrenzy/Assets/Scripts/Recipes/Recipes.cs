using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipes : MonoBehaviour
{
    public Sprite applePieSprite;
    public Sprite fruitBowlSprite;
    public Sprite fruitSmoothieSprite;

    private static Dictionary<RecipeName, Dictionary<string, object>> recipes = new Dictionary<RecipeName, Dictionary<string, object>>()
    {
        {
            RecipeName.applePie, new Dictionary<string, object>
            {
                { "ingredients", new List<FoodName> { FoodName.apple, FoodName.milk, FoodName.flour, FoodName.egg } },
                { "deliveryTime", 10 },
                { "points", 50 },
                { "image", Resources.Load<Sprite>("Sprites/applePie") }
            }
        },
        {
            RecipeName.fruitBowl, new Dictionary<string, object>
            {
                { "ingredients", new List<FoodName> { FoodName.strawberry, FoodName.apple, FoodName.banana } },
                { "deliveryTime", 10 },
                { "points", 10 },
                { "image", Resources.Load<Sprite>("Sprites/fruitBowl") }
            }
        },
        {
            RecipeName.fruitSmoothie, new Dictionary<string, object>
            {
                { "ingredients", new List<FoodName> { FoodName.strawberry, FoodName.banana, FoodName.milk } },
                { "deliveryTime", 10 },
                { "points", 20 },
                { "image", Resources.Load<Sprite>("Sprites/fruitSmoothie")}
            }
        }
    };

    void Start()
    {

    }

    #region Getters and setters
    public static Dictionary<RecipeName, Dictionary<string, object>> GetRecipes()
    {
        return recipes;
    }
    #endregion
}
