// Food.cs
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] protected FoodName foodName;
    [SerializeField] protected FoodStatus foodStatus;
    [SerializeField] protected FoodType foodType;

    #region Getters and setters

    public void SetFoodStatus(FoodStatus newFoodSatus)
    {
        foodStatus = newFoodSatus;
    }

    public FoodStatus GetFoodStatus()
    {
        return foodStatus;
    }

    public FoodName GetFoodName()
    {
        return foodName;
    }

    public FoodType GetFoodType()
    {
        return foodType;
    }
    #endregion
}
