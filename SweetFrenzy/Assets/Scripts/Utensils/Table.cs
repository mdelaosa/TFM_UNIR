using UnityEngine;

public class Table : Utensil
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Food food = other.GetComponent<Food>();
            if (food != null && food.GetFoodStatus() == FoodStatus.ready && food.GetFoodType() == FoodType.cooked)
            {
                food.SetFoodStatus(FoodStatus.served);
                Debug.Log($"Food {food.GetFoodName()} is now served.");

                OrderManager.Instance.FulfillOrder(other.gameObject);
            }
        }
    }
}
