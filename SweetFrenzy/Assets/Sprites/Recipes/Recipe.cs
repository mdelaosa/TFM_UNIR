using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    [SerializeField] private RecipeName recipeName;
    [SerializeField] private List<FoodName> ingredients = new List<FoodName>();

    [SerializeField] private Dictionary<RecipeName, List<FoodName>> recipeIngredients = new Dictionary<RecipeName, List<FoodName>>()
    {
        { RecipeName.applePie, new List<FoodName> { FoodName.apple, FoodName.milk, FoodName.flour, FoodName.egg } },
        { RecipeName.fruitBowl, new List<FoodName> { FoodName.strawberry, FoodName.apple, FoodName.banana } },
        { RecipeName.fruitSmoothie, new List<FoodName> { FoodName.strawberry, FoodName.banana, FoodName.milk } }
    };

    [SerializeField] private bool isReady;

    void Start()
    {
        isReady = false;
        AssignIngredients();
    }

    void Update() { }

    public void AssignIngredients()
    {
        if (recipeIngredients.ContainsKey(recipeName))
        {
            ingredients = recipeIngredients[recipeName];
        }
        else
        {
            Debug.LogWarning("No se encontraron ingredientes para la receta: " + recipeName);
        }
    }

    #region Getters and setters
    public List<FoodName> GetIngredients()
    {
        return ingredients;
    }
    #endregion
}