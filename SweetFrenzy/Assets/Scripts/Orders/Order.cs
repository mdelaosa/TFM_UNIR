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
    [SerializeField] private Sprite image;
    [SerializeField] private bool isReady;

    // Nueva variable para almacenar la receta
    private Dictionary<string, object> recipe; // Aquí se guardará la receta

    private float timer;
    private GameManager gameManager;
    private OrderManager orderManager;

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

        orderManager = FindObjectOfType<OrderManager>();
        orderManager.AddOrder(this);
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
            // Guardar la receta en la variable global
            recipe = recipes[recipeName];

            // Asignar los valores desde la receta
            ingredients = recipe["ingredients"] as List<FoodName>;
            deliveryTime = (int)recipe["deliveryTime"];
            points = (int)recipe["points"];
            image = (Sprite)recipe["image"];
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
                gameManager.AddPoints(-(points / 2));
            }
            Destroy(gameObject);
        }
    }

    public bool FulfillOrder(List<FoodName> servedFoods)
    {
        if (isReady)
            return false;

        foreach (var ingredient in ingredients)
        {
            if (!servedFoods.Contains(ingredient))
                return false;
        }

        isReady = true;
        Debug.Log($"Order {idOrder} is fulfilled!");
        if (gameManager != null)
        {
            gameManager.AddPoints(points);
        }

        Destroy(gameObject);
        return true;
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

    public Sprite GetImageRecipe()
    {
        return image;
    }

    public void SetRecipeName(RecipeName newRecipeName)
    {
        recipeName = newRecipeName;
        AssignRecipe();
    }

    public Dictionary<string, object> GetRecipe()
    {
        return recipe;
    }

    #endregion
}
