using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    [SerializeField] private RecipeName recipeName;
    [SerializeField] private List<FoodName> ingredients = new List<FoodName>();
    [SerializeField] private int deliveryTime;
    [SerializeField] private int points;
    [SerializeField] private bool isReady;
    private float timer;
    private GameManager gameManager;

    [SerializeField]
    private Dictionary<RecipeName, Dictionary<string, object>> recipes = new Dictionary<RecipeName, Dictionary<string, object>>()
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

    void Start()
    {
        isReady = false;
        AssignRecipe();
        StartTimer();

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager no encontrado en la escena.");
        }
    }

    void Update()
    {
        UpdateTimer();
    }

    public void AssignRecipe()
    {
        if (recipes.ContainsKey(recipeName))
        {
            var recipeData = recipes[recipeName];
            ingredients = recipeData["ingredients"] as List<FoodName>;
            deliveryTime = (int)recipeData["deliveryTime"];
            points = (int)recipeData["points"];
        }
    }

    #region Create recipe
    public static Recipe CreateRecipe(GameObject parentObject, RecipeName recipeName)
    {
        Recipe newRecipe = parentObject.AddComponent<Recipe>();
        newRecipe.SetRecipeName(recipeName);
        return newRecipe;
    }
    #endregion

    #region Timer
    private void StartTimer()
    {
        timer = deliveryTime;
    }

    private void UpdateTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                CheckOrderStatus();
            }
        }
    }

    private void CheckOrderStatus()
    {
        if (!isReady)
        {
            Debug.Log("Pedido perdido: " + recipeName);

            if (gameManager != null && recipes.ContainsKey(recipeName))
            {
                gameManager.AddPoints(-points); 
            }
        }
    }
    #endregion

    #region Getters and setters
    public List<FoodName> GetIngredients()
    {
        return ingredients;
    }

    public int GetDeliveryTime()
    {
        return deliveryTime;
    }

    public void SetRecipeName(RecipeName newRecipeName)
    {
        recipeName = newRecipeName;
        AssignRecipe();
    }
    #endregion
}
