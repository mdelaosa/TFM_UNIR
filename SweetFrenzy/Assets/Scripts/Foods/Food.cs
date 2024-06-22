// Food.cs
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] protected FoodName foodName;
    [SerializeField] protected FoodStatus foodStatus;
    [SerializeField] protected FoodType foodType;
    //[SerializeField] private string foodTag = "Food";

    void Start()
    {
        //gameObject.tag = foodTag;
    }

    void Update()
    {
    }

    #region Getters and setters

    public FoodStatus GetFoodStatus()
    {
        return foodStatus;
    }

    public FoodName GetFoodName()
    {
        return foodName;
    }
    #endregion
}
