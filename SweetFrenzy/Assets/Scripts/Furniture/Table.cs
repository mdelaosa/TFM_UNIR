using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private TableID tableID;

    [SerializeField] private List<Order> activeOrders = new List<Order>();

    public void AddOrder(Order order)
    {
        activeOrders.Add(order);
    }

    public void RemoveOrder(Order order)
    {
        activeOrders.Remove(order);
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

                break;
            }
        }
    }

    private void OnColliderEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Food food = other.GetComponent<Food>();
            if (food != null && food.GetFoodStatus() == FoodStatus.ready && food.GetFoodType() == FoodType.cooked)
            {
                food.SetFoodStatus(FoodStatus.served);
                Debug.Log($"Food {food.GetFoodName()} is now served.");

                FulfillOrder(other.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}
