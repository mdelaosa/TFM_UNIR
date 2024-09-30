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

    private ClientController client;

    private Dictionary<string, object> recipe;

    private float timer;
    private GameManager gameManager;

    void Start()
    {
        isReady = false;
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
            recipe = recipes[recipeName];

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
            if (gameManager != null)
            {
                gameManager.AddPoints(-5);
            }
        }
    }
  

    public static Order CreateOrder(GameObject parentObject, RecipeName recipeName, ClientController client)
    {
        Order newOrder = parentObject.AddComponent<Order>();
        

        var recipes = Recipes.GetRecipes();
        if (recipes.ContainsKey(recipeName))
        {
            Dictionary<string, object> recipe = recipes[recipeName];

            newOrder.SetIngredients(recipe["ingredients"] as List<FoodName>);
            newOrder.SetDeliveryTime((int)recipe["deliveryTime"]);
            newOrder.SetPoints((int)recipe["points"]);
            newOrder.SetImageRecipe((Sprite)recipe["image"]);
        }

        newOrder.SetRecipeName(recipeName);
        newOrder.SetClient(client);

        return newOrder;
    }

    #region Getters and setters

    public void SetIsReady(bool newIsReady)
    {
        isReady = newIsReady;

        if (isReady )
        {
            Debug.Log($"Order {idOrder} is fulfilled!");
            if (gameManager != null)
            {
                gameManager.AddPoints(points);
            }
        }
    }

    public void SetIngredients(List<FoodName> list)
    {
        ingredients = list;
    }

    public int GetDeliveryTime()
    {
        return deliveryTime;
    }

    public void SetDeliveryTime(int newDevileryTime)
    {
        deliveryTime = newDevileryTime;
    }

    public void SetPoints(int newPoints)
    {
        points = newPoints;
    }

    public Sprite GetImageRecipe()
    {
        return image;
    }

    public void SetImageRecipe(Sprite newImage)
    {
        image = newImage;
    }

    public RecipeName GetRecipeName()
    {
        return recipeName;
    }

    public void SetRecipeName(RecipeName newRecipeName)
    {
        recipeName = newRecipeName;
        AssignRecipe();
    }

    public void SetClient(ClientController newClient)
    {
        client = newClient;
    }

    public ClientController GetClient()
    {
        return client;
    }

    #endregion
}
