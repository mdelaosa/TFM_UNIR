using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] private int idOrder;
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
        else
        {
            gameManager.AddOrder();
            idOrder = gameManager.GetNumOrders();
        }
    }

    void Update()
    {
        UpdateTimer();
    }

    public void AssignRecipe()
    {
        var recipes = Recipes.GetRecipes();
        if (recipes.ContainsKey(recipeName))
        {
            var recipeData = recipes[recipeName];
            ingredients = recipeData["ingredients"] as List<FoodName>;
            deliveryTime = (int)recipeData["deliveryTime"];
            points = (int)recipeData["points"];
        }
    }

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
    #endregion

    private void CheckOrderStatus()
    {
        if (!isReady)
        {
            Debug.Log("Pedido perdido: " + idOrder + " - " + recipeName);

            if (gameManager != null)
            {
                gameManager.AddPoints(-(points/2));
            }
            Destroy(gameObject);
        }
    }

    #region Getters and setters

    public static Order CreateOrder(GameObject parentObject, RecipeName recipeName)
    {
        Order newOrder = parentObject.AddComponent<Order>();
        newOrder.SetRecipeName(recipeName);
        return newOrder;
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
    #endregion
}
