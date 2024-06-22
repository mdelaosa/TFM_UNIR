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
        var recipes = RecipeGenerator.GetRecipes();
        if (recipes.ContainsKey(recipeName))
        {
            var recipeData = recipes[recipeName];
            ingredients = recipeData["ingredients"] as List<FoodName>;
            deliveryTime = (int)recipeData["deliveryTime"];
            points = (int)recipeData["points"];
        }
    }

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

            if (gameManager != null)
            {
                gameManager.AddPoints(-points);
            }
        }
    }

    public static Recipe CreateRecipe(GameObject parentObject, RecipeName recipeName)
    {
        Recipe newRecipe = parentObject.AddComponent<Recipe>();
        newRecipe.SetRecipeName(recipeName);
        return newRecipe;
    }

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
}
