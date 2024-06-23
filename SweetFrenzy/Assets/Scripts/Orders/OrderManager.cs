using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    private List<Order> activeOrders = new List<Order>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddOrder(Order order)
    {
        activeOrders.Add(order);
        foreach (var order1 in activeOrders)
        {
            Debug.Log("-----------------------------------------------" + order1);
        }
    }

    public void FulfillOrder(GameObject other)
    {
        Food food = other.GetComponent<Food>();
        List<FoodName> servedFood = new List<FoodName> { food.GetFoodName() };

        foreach (var order in activeOrders)
        {
            if (order.FulfillOrder(servedFood))
            {
                activeOrders.Remove(order);
                //Destroy(other);

                break; 
            }
        }
    }
}
